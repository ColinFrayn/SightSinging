using System;
using System.Windows.Forms;
using System.Drawing;
using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace SightSinging {
  public partial class MainForm : Form {
    #region Typedefs
    private enum Clef { Treble, Treble_8, Bass };
    private enum Voice { Soprano, Alto, Tenor, Bass };
    private enum Accidental { None, Natural, Sharp, Flat, DoubleSharp, DoubleFlat };
    #endregion // Typedefs
    #region Settings
    private OutputDevice outDevice;
    private int outDeviceID = 0;
    private OutputDeviceDialog outDialog = new OutputDeviceDialog();
    private int iKeySignature = 0; // 0 = Cmaj, +ve = sharps (e.g. 1=G, 2=D etc.) and -ve = flats (1=F, 2=Bflat). Not bothering with minors.
    private int yStaveGap = 0, xBorder = 0, yBorder = 0;
    private const int iNoteSize = 11;
    private const float fNoteSquash = 0.8f;
    private readonly List<Note> lNotes = new List<Note>();
    private Clef ThisClef = Clef.Treble;
    private Voice ThisVoice = Voice.Soprano;
    private const int MaxNotes = 13; // Maximum #notes that can fit on the score
    private Random rand = new Random();
    private int iPlayingNote = -1;
    private int iDiff = 0;
    private Timer tPlayer;  // Timer for playback
    private bool bCue = false;
    #endregion // Settings
    #region Music Config
    private const int LowerSop = 62, UpperSop = 77;
    private const int LowerAlto = 57, UpperAlto = 73;
    private const int LowerTenor = 50, UpperTenor = 65;
    private const int LowerBass = 43, UpperBass = 61;
    private readonly int[] SharpPositions = new int[] { 8, 5, 9, 6, 3, 7, 4 };
    private readonly int[] FlatPositions = new int[] { 4, 7, 3, 6, 2, 5, 1 };
    private readonly HashSet<int> hsPreferSharp = new HashSet<int> { 1, 5, 2, 6 };
    private readonly Dictionary<int, HashSet<int>> dKeySigToAccidentals = new Dictionary<int, HashSet<int>>();
    private readonly Dictionary<int,int> dIntervals = new Dictionary<int,int> { { 2, 0 }, { 1, 0 }, { 4, 10 }, { 3, 10 }, { 12, 30 }, { 7, 35 }, { 5, 40 }, { 9, 45 }, { 11, 50 }, { 10, 55 }, { 6, 60 }, { 8, 65 }, { 14, 70 }, { 13, 75 }, { 16, 80 }, { 15, 85 } };
    private readonly List<int> lDiffToMaxInterval = new List<int> { 4, 5, 7, 8, 11, 20 };
    private readonly Dictionary<int, int> dKeyToRootNote = new Dictionary<int, int>();
    #endregion // Music Config
    #region DrawingStuff
    private readonly Bitmap bmpTrebleClef, bmpBassClef, bmpTreble8Clef, bmpFlat, bmpSharp, bmpNatural;
    private const double TrebleClefWidth = 50, TrebleClefHeight = 160;
    private const double Treble8ClefWidth = 50, Treble8ClefHeight = 183;
    private const double BassClefWidth = 55, BassClefHeight = 70;
    private const double FlatWidth = 17, FlatHeight = 46, FlatTopOffset = 34;
    private const double SharpWidth = 16, SharpHeight = 45, SharpTopOffset = 21;
    private const double NaturalWidth = 12, NaturalHeight = 44, NaturalTopOffset = 21;
    private const double AccidentalScale = 0.8;
    #endregion //DrawingStuff
    #region Accessors
    private double DrawScale { get { return (double)pbScore.Height / (double)245; } }
    private int NoteSpacing { get { return (int)((double)((pbScore.Width - xBorder*2 - FirstNoteX) / (double)(MaxNotes-1))); } }
    private int AccidentalSpacing { get { return (int)((double)pbScore.Width * 0.02); } }
    private int AfterClefX { get { return xBorder + (int)((double)pbScore.Width * (ThisClef == Clef.Bass ? 0.066 : 0.055)); } }
    private int FirstNoteX { get { return AfterClefX + (Math.Abs(iKeySignature) * AccidentalSpacing) + (int)((double)pbScore.Width * 0.055); } }
    private int TickInterval { get { return 18000 / (tbSpeed.Value + 5); } }
    #endregion // Accessors

    public MainForm() {
      InitializeComponent();
      bmpTrebleClef = new Bitmap(Properties.Resources.TrebleClef);
      bmpTrebleClef.MakeTransparent(Color.White);
      bmpTreble8Clef = new Bitmap(Properties.Resources.TrebleClef8);
      bmpTreble8Clef.MakeTransparent(Color.White);
      bmpBassClef = new Bitmap(Properties.Resources.BassClef);
      bmpBassClef.MakeTransparent(Color.White);
      bmpFlat = new Bitmap(Properties.Resources.Flat);
      bmpFlat.MakeTransparent(Color.White);
      bmpSharp = new Bitmap(Properties.Resources.Sharp);
      bmpSharp.MakeTransparent(Color.White);
      bmpNatural = new Bitmap(Properties.Resources.Natural);
      bmpNatural.MakeTransparent(Color.White);
      pbScore_Resize(null, null);
      sopranoToolStripMenuItem.Checked = true;
      altoToolStripMenuItem.Checked = false;
      tenorToolStripMenuItem.Checked = false;
      bassToolStripMenuItem.Checked = false;
      veryEasyToolStripMenuItem.Checked = true;
      easyToolStripMenuItem.Checked = false;
      mediumToolStripMenuItem.Checked = false;
      hardToolStripMenuItem.Checked = false;
      advancedToolStripMenuItem.Checked = false;
      expertToolStripMenuItem.Checked = false;
      tbSpeed.Value = 20;
      // Set up a few lookup tables
      dKeySigToAccidentals.Add(0, new HashSet<int>());
      HashSet<int> hsTemp = new HashSet<int>();
      for (int i = 0; i < SharpPositions.Length; i++) {
        hsTemp.Add(SharpPositions[i] % 7);
        dKeySigToAccidentals.Add(i + 1, new HashSet<int>(hsTemp));
      }
      hsTemp.Clear();
      for (int i = 0; i < FlatPositions.Length; i++) {
        hsTemp.Add(FlatPositions[i] % 7);
        dKeySigToAccidentals.Add(-(i + 1), new HashSet<int>(hsTemp));
      }
      for (int i=-7; i<=7; i++) {
        dKeyToRootNote.Add(i, (60 + (7 * i)) % 12);
      }
      // Disable playback until a tune is generated
      btCue.Enabled = false;
      btPlay.Enabled = false;
      btStop.Enabled = false;
      btNext.Enabled = false;
      btLast.Enabled = false;
    }

    #region Message Handlers
    private void MainForm_Load(object sender, EventArgs e) {
      typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, pbScore, new object[] { true });

      if (OutputDevice.DeviceCount == 0) {
        MessageBox.Show("No MIDI output devices available.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        Close();
      }
      else {
        try {
          outDevice = new OutputDevice(outDeviceID);
        }
        catch (Exception ex) {
          MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
          Close();
        }
      }
    }
    private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
      if (outDevice != null) outDevice.Dispose();
      outDialog.Dispose();
    }
    private void pbScore_Paint(object sender, PaintEventArgs e) {
      Graphics g = e.Graphics;
      g.Clear(Color.FromArgb(255, 248, 235));
      g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

      // Draw stave
      for (int i = 0; i <= 4; i++) {
        g.DrawLine(new Pen(Color.Black), xBorder, yBorder + (i * yStaveGap), pbScore.Width - xBorder, yBorder + (i * yStaveGap));
      }
      // Draw clef & key signature
      DrawClef(g, ThisClef, xBorder);
      DrawKeySignature(g);

      // Draw notes
      for (int n = 0; n < lNotes.Count; n++) {
        DrawNote(g, lNotes[n], FirstNoteX + (NoteSpacing * n), n == iPlayingNote);
      }
      g.Flush();
    }
    private void pbScore_Resize(object sender, EventArgs e) {
      xBorder = pbScore.Width / 20;
      yBorder = (pbScore.Height / 3);
      yStaveGap = (pbScore.Height - (yBorder * 2)) / 4;
      this.Invalidate(true);
    }
    #endregion // Message Handlers

    #region Playback
    private void btNext_Click(object sender, EventArgs e) {
      if (lNotes.Count == 0) return;
      PlayTick(null, null);
    }
    private void btLast_Click(object sender, EventArgs e) {
      if (lNotes.Count == 0) return;
      if (iPlayingNote < 1) return;
      if (iPlayingNote >= 0 && iPlayingNote < lNotes.Count) outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, lNotes[iPlayingNote].NotePitch, 0));
      iPlayingNote--;
      outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, lNotes[iPlayingNote].NotePitch, tbVolume.Value));
      this.Invalidate(true);
    }
    private void btStop_Click(object sender, EventArgs e) {
      if (tPlayer != null) {
        tPlayer.Stop();
        tPlayer.Dispose();
      }
      iPlayingNote = -1;
      btPlay.Text = "Play";
      this.Invalidate(true);
    }
    private void btPlay_Click(object sender, EventArgs e) {
      if (lNotes.Count == 0) return;
      bCue = false;
      if (tPlayer == null || !tPlayer.Enabled) {
        if (tPlayer != null) tPlayer.Dispose();
        tPlayer = new System.Windows.Forms.Timer();
        tPlayer.Tick += new EventHandler(PlayTick);
        tPlayer.Interval = TickInterval;
        tPlayer.Start();
        PlayTick(null, null);
        btPlay.Text = "Pause";
      }
      else {
        if (tPlayer != null) tPlayer.Stop();
        btPlay.Text = "Play";
      }
      this.Invalidate(true);
    }
    private void btCue_Click(object sender, EventArgs e) {
      if (lNotes.Count == 0) return;
      if (iPlayingNote >= 0 && iPlayingNote < lNotes.Count) outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, lNotes[iPlayingNote].NotePitch, 0));
      if (tPlayer != null) tPlayer.Dispose();
      iPlayingNote = -1;
      bCue = true;
      tPlayer = new System.Windows.Forms.Timer();
      tPlayer.Tick += new EventHandler(PlayTick);
      tPlayer.Interval = TickInterval;
      tPlayer.Start();
      PlayTick(null, null);
    }
    private void PlayTick(Object obj, EventArgs args) {
      if (iPlayingNote >= 0 && iPlayingNote < lNotes.Count) outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, lNotes[iPlayingNote].NotePitch, 0));
      iPlayingNote++;
      if (iPlayingNote >= lNotes.Count || (iPlayingNote>0 && bCue)) {
        tPlayer.Stop();
        tPlayer.Dispose();
        iPlayingNote = -1;
        btPlay.Text = "Play";
      }
      else outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, lNotes[iPlayingNote].NotePitch, tbVolume.Value));
      this.Invalidate(true);
    }
    private void tbSpeed_Scroll(object sender, EventArgs e) {
      if (tPlayer != null) tPlayer.Interval = TickInterval;
    }
    #endregion // Playback

    #region Generate Test
    private void btRandom_Click(object sender, EventArgs e) {
      Tuple<int,int> tpRange = GetRange(ThisVoice, iDiff);

      // Get a random key signature, suitable for the difficulty level
      do {
        iKeySignature = rand.Next(iDiff + 1) + rand.Next(iDiff + 1);
      } while (iKeySignature > 7);
      if (rand.NextDouble() > 0.5) iKeySignature = -iKeySignature;

      // Set up the notes in the test
      List<int> lPitches = GenerateTest(tpRange.Item1, tpRange.Item2, iDiff);

      // Now configure the note positions, accidentals etc.
      SetupNotes(lPitches);

      // Reset & redraw everything
      if (tPlayer != null) {
        tPlayer.Stop();
        tPlayer.Dispose();
      }
      iPlayingNote = -1;
      btPlay.Text = "Play";
      btCue.Enabled = true;
      btPlay.Enabled = true;
      btStop.Enabled = true;
      btNext.Enabled = true;
      btLast.Enabled = true;
      this.Invalidate(true);
    }
    private List<int> GenerateTest(int iLow, int iHigh, int iDiff) {
      List<int> lPitches = new List<int>();
      List<int> lIntervals = new List<int>(dIntervals.Keys);
      int iIntervalMax = Math.Min(lDiffToMaxInterval[iDiff], dIntervals.Count);
      int CountIntMax = 0, CountInt2ndMax = 0, CountAccidental = 0, CountHigh = 0;

      // Start off with a note in the correct key
      int iLast = 0;
      bool bOK = false;
      do {
        bOK = false;
        iLast = iLow + rand.Next(iHigh - iLow + 1);
        if (iLast % 12 == dKeyToRootNote[iKeySignature]) bOK = true;
        if (iLast % 12 == (dKeyToRootNote[iKeySignature] + 4) % 12) bOK = true;
        if (iLast % 12 == (dKeyToRootNote[iKeySignature] + 7) % 12) bOK = true;
        if (Math.Abs(iLast - iLow) < 2 || Math.Abs(iLast - iHigh) < 2) bOK = false;
      } while (!bOK || CalculateAccidentalAndShift(iLast, iKeySignature).Item1 != Accidental.None);
      lPitches.Add(iLast);

      for (int i = 1; i < MaxNotes; i++) {
        int iNote = iLast, iInterval;
        Tuple<Accidental, bool> tpAcc;
        do {
          iInterval = rand.Next(iIntervalMax);
          bool bDirection = (rand.NextDouble() > 0.5);
          iNote = iLast + lIntervals[iInterval] * (bDirection ? 1 : -1);
          // Occasionally repeat the previous-but-one note, to make things a bit easier
          if (i > 1 && iDiff < 5 && rand.NextDouble() > 0.5 && rand.Next(10) > (iDiff + 3)) iNote = lPitches[i - 1];
          // Check if this note has any accidentals (i.e. is not in the key signature)
          tpAcc = CalculateAccidentalAndShift(iNote, iKeySignature);
          // See how likely we are to choose this next note. More conservative on low difficulty.
          int iAccept = 20 + (iDiff * 15) - lIntervals[iInterval];
          // Repeated notes = easier (especially if in the root scale), but don't repeat too much
          iAccept += (5-iDiff) * lPitches.Where(x => x == iNote).Count() * (tpAcc.Item1 == Accidental.None ? 2 : 1);
          if (i > 2 && iNote == lPitches[i - 2] && lPitches[i - 1] == lPitches[i - 3]) iAccept -= 50; 
          // Note is in root triad
          if (iNote % 12 == dKeyToRootNote[iKeySignature]) iAccept += 10;
          if (iNote % 12 == (dKeyToRootNote[iKeySignature]+4)%12) iAccept += 10;
          if (iNote % 12 == (dKeyToRootNote[iKeySignature]+7)%12) iAccept += 10;
          // Avoid multiple repetitions of difficult intervals
          if (iInterval == iIntervalMax - 1) iAccept -= (CountIntMax * 10) + 10;
          if (iInterval == iIntervalMax - 2) iAccept -= (CountInt2ndMax * 5) + 5;
          // Avoid going close to edges of range
          if (iNote == iLow || iNote == iHigh) iAccept -= 15;
          if (iNote == iLow + 1 || iNote == iHigh - 1) iAccept -= 10;
          if (iNote == iLow + 2 || iNote == iHigh - 2) iAccept -= 5;
          // Try not to leap up to high notes
          if (lIntervals[iInterval] > 8 && iNote == iHigh) iAccept -= lIntervals[iInterval];
          if (lIntervals[iInterval] > 8 && iNote >= iHigh - 1) iAccept -= lIntervals[iInterval];
          // Avoid too many high notes
          if (iNote >= iHigh - 1) iAccept -= CountHigh++;
          if (iNote == iHigh) iAccept -= CountHigh++;
          // Avoid difficult accidentals / too many accidentals
          if (tpAcc.Item1 == Accidental.None) iAccept += 35;
          else {
            // If this has an accidental, then see if it's actually in a related key
            if (iKeySignature > -7) {
              Tuple<Accidental, bool> tpAccSubdominant = CalculateAccidentalAndShift(iNote, iKeySignature - 1);
              if (tpAccSubdominant.Item1 != Accidental.None) iAccept -= 15;
            }
            if (iKeySignature < 7) {
              Tuple<Accidental, bool> tpAccDominant = CalculateAccidentalAndShift(iNote, iKeySignature + 1);
              if (tpAccDominant.Item1 != Accidental.None) iAccept -= 15;
            }
            iAccept -= (5 * CountAccidental);
            if (iDiff == 0) iAccept = 0;
            if (tpAcc.Item1 == Accidental.DoubleFlat) iAccept -= 15;
            if (tpAcc.Item1 == Accidental.DoubleSharp) iAccept -= 15;
          }
          // If this note is too complicated given the context then try again
          if (rand.Next(100) > iAccept && iDiff < 5) iNote = 999;
        } while (iNote > iHigh || iNote < iLow || iNote == iLast);
        lPitches.Add(iNote);
        iLast = iNote;
        if (iInterval == iIntervalMax - 1) CountIntMax++;
        if (iInterval == iIntervalMax - 2) CountInt2ndMax++;
        if (tpAcc.Item1 != Accidental.None) CountAccidental++;
      }

      return lPitches;
    }
    private void SetupNotes(List<int> lPitches) {
      lNotes.Clear();
      Dictionary<int, Accidental> dLastAcc = new Dictionary<int, Accidental>();
      foreach (int iNote in lPitches) {
        Note nt = new Note();
        nt.NotePitch = iNote;
        Tuple<Accidental, bool> tp = CalculateAccidentalAndShift(iNote, iKeySignature);
        nt.Line = CalculateWhiteNoteSteps(ClefToBottomLine(ThisClef), iNote);
        if (tp.Item2) nt.Line++;
        nt.Accidental = tp.Item1;
        // Tweak accidental if we're cancelling a previous one
        if (dLastAcc.ContainsKey(nt.Line)) {
          if (nt.Accidental == Accidental.None && dLastAcc[nt.Line] != Accidental.None) {
            bool bBlack = IsBlackNote(iNote);
            int st = (nt.Line + 7) % 7;
            //if (bBlack && iKeySignature < 0) st++;
            //st = st % 7;
            if (iKeySignature > 0 && dKeySigToAccidentals[iKeySignature].Contains(st)) nt.Accidental = Accidental.Sharp;
            else if (iKeySignature < 0 && dKeySigToAccidentals[iKeySignature].Contains(st)) nt.Accidental = Accidental.Flat;
            else nt.Accidental = Accidental.Natural;
            dLastAcc[nt.Line] = Accidental.None; // No need to cancel it next time
          }
          else if (dLastAcc[nt.Line] == nt.Accidental) nt.Accidental = Accidental.None;
          else dLastAcc[nt.Line] = nt.Accidental;          
        }
        else dLastAcc[nt.Line] = nt.Accidental;
        lNotes.Add(nt);
      }
    }
    private Tuple<Accidental, bool> CalculateAccidentalAndShift(int iNote, int iKeySignature) {
      bool bBlack = IsBlackNote(iNote);
      int st = CalculateWhiteNoteSteps(ClefToBottomLine(ThisClef), iNote + 12);
      if (bBlack && iKeySignature < 0) st++;
      st = st % 7;

      if (bBlack) {  // Do we need a sharp/flat sign?
        if (dKeySigToAccidentals[iKeySignature].Contains(st)) return new Tuple<Accidental, bool>(Accidental.None, iKeySignature < 0);
        if (iKeySignature > 0) return new Tuple<Accidental, bool>(Accidental.Sharp, false);
        else if (iKeySignature < 0) return new Tuple<Accidental, bool>(Accidental.Flat, true);
        else { // Choose sharps or flats based on which would be more likely
          if (hsPreferSharp.Contains(st)) return new Tuple<Accidental, bool>(Accidental.Sharp, false);
          return new Tuple<Accidental, bool>(Accidental.Flat, true);
        }
      }
      else { // It's a white note. Do we need a natural sign?
        if (dKeySigToAccidentals[iKeySignature].Contains(st)) return new Tuple<Accidental, bool>(Accidental.Natural, false);
      }
      return new Tuple<Accidental, bool>(Accidental.None, false);
    }
    private static Tuple<int,int> GetRange(Voice v, int d) {
      int low, high;
      switch (v) {
        case Voice.Soprano: low = LowerSop; high = UpperSop; break;
        case Voice.Alto: low = LowerAlto; high = UpperAlto; break;
        case Voice.Tenor: low = LowerTenor; high = UpperTenor; break;
        case Voice.Bass: low = LowerBass; high = UpperBass; break;
        default: throw new NotImplementedException();
      }
      if (d > 0) { low--; high++; }
      if (d > 1) { low--; high++; }
      if (d > 2) { low--; high++; }
      return new Tuple<int, int>(low, high);
    }
    #endregion //Generate Test

    #region Menu Items
    // Change voice part
    private void sopranoToolStripMenuItem_Click(object sender, EventArgs e) {
      sopranoToolStripMenuItem.Checked = true;
      altoToolStripMenuItem.Checked = false;
      tenorToolStripMenuItem.Checked = false;
      bassToolStripMenuItem.Checked = false;
      if (ThisVoice != Voice.Soprano) lNotes.Clear();
      ThisVoice = Voice.Soprano;
      ThisClef = Clef.Treble;
      this.Invalidate(true);
    }
    private void altoToolStripMenuItem_Click(object sender, EventArgs e) {
      sopranoToolStripMenuItem.Checked = false;
      altoToolStripMenuItem.Checked = true;
      tenorToolStripMenuItem.Checked = false;
      bassToolStripMenuItem.Checked = false;
      if (ThisVoice != Voice.Alto) lNotes.Clear();
      ThisVoice = Voice.Alto;
      ThisClef = Clef.Treble;
      this.Invalidate(true);
    }
    private void tenorToolStripMenuItem_Click(object sender, EventArgs e) {
      sopranoToolStripMenuItem.Checked = false;
      altoToolStripMenuItem.Checked = false;
      tenorToolStripMenuItem.Checked = true;
      bassToolStripMenuItem.Checked = false;
      if (ThisVoice != Voice.Tenor) lNotes.Clear();
      ThisVoice = Voice.Tenor;
      ThisClef = Clef.Treble_8;
      this.Invalidate(true);
    }
    private void bassToolStripMenuItem_Click(object sender, EventArgs e) {
      sopranoToolStripMenuItem.Checked = false;
      altoToolStripMenuItem.Checked = false;
      tenorToolStripMenuItem.Checked = false;
      bassToolStripMenuItem.Checked = true;
      if (ThisVoice != Voice.Bass) lNotes.Clear();
      ThisVoice = Voice.Bass;
      ThisClef = Clef.Bass;
      this.Invalidate(true);
    }

    // Set the tune difficulty
    private void veryEasyToolStripMenuItem_Click(object sender, EventArgs e) {
      iDiff = 0;
      SetDifficultyTicks();
    }
    private void easyToolStripMenuItem_Click(object sender, EventArgs e) {
      iDiff = 1;
      SetDifficultyTicks();
    }
    private void mediumToolStripMenuItem_Click(object sender, EventArgs e) {
      iDiff = 2;
      SetDifficultyTicks();
    }
    private void hardToolStripMenuItem_Click(object sender, EventArgs e) {
      iDiff = 3;
      SetDifficultyTicks();
    }
    private void advancedToolStripMenuItem_Click(object sender, EventArgs e) {
      iDiff = 4;
      SetDifficultyTicks();
    }
    private void expertToolStripMenuItem_Click(object sender, EventArgs e) {
      iDiff = 5;
      SetDifficultyTicks();
    }
    private void SetDifficultyTicks() {
      veryEasyToolStripMenuItem.Checked = (iDiff == 0);
      easyToolStripMenuItem.Checked = (iDiff == 1);
      mediumToolStripMenuItem.Checked = (iDiff == 2);
      hardToolStripMenuItem.Checked = (iDiff == 3);
      advancedToolStripMenuItem.Checked = (iDiff == 4);
      expertToolStripMenuItem.Checked = (iDiff == 5);
    }

    // Miscellaneous menu stuff
    private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
      this.Close();
    }
    private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
      MessageBox.Show("SightSinging v1.0\nBy Colin Frayn\n(c) 2017\ncolin@frayn.net", "About");
    }
    #endregion // Menu Items

    #region Drawing Methods
    private int ConvertNoteToYCoord(int iNote, Clef clef) {
      int iBottomLine = ClefToBottomLine(clef);
      int iWhiteNoteSteps = CalculateWhiteNoteSteps(iBottomLine, iNote);
      return yBorder + (4 * yStaveGap) - (iWhiteNoteSteps * yStaveGap / 2);
    }
    private void DrawNote(Graphics g, Note note, int xPos, bool bHighlight) {
      int yPos = yBorder + (4 * yStaveGap) - (note.Line * yStaveGap / 2);
      DrawNote(g, xPos, yPos, bHighlight, note.Accidental);
    }
    private void DrawNote(Graphics g, int x, int y, bool bHighlight, Accidental acc) {
      bool bUpper = (y < yBorder + (2 * yStaveGap));
      float iNoteSizeMod = iNoteSize * (float)DrawScale;

      // Note stalk
      Pen bPen = new Pen(Color.Black, 3);
      if (bUpper) g.DrawLine(bPen, x - iNoteSizeMod + 2, y, x - iNoteSizeMod + 2, y + yStaveGap * 3);
      else g.DrawLine(bPen, x + iNoteSizeMod - 2, y, x + iNoteSizeMod - 2, y - yStaveGap * 3);

      // Note head
      //g.RotateTransform(-25.0f);
      if (bHighlight) {
        g.FillEllipse(new SolidBrush(Color.Red), x - iNoteSizeMod, y - (iNoteSizeMod * fNoteSquash), iNoteSizeMod * 2.0f, iNoteSizeMod * fNoteSquash * 2.0f);
      }
      else {
        g.FillEllipse(new SolidBrush(Color.Black), x - iNoteSizeMod, y - (iNoteSizeMod * fNoteSquash), iNoteSizeMod * 2.0f, iNoteSizeMod * fNoteSquash * 2.0f);
      }
      //g.RotateTransform(25.0f);

      // Ledger lines
      if (y < yBorder - (yStaveGap / 2)) {
        for (int yLine = yBorder - yStaveGap; yLine >= y; yLine -= yStaveGap) {
          g.DrawLine(new Pen(Color.Black), x - (iNoteSizeMod * 2), yLine, x + (iNoteSizeMod * 2), yLine);
        }
      }
      if (y > yBorder + (yStaveGap * 9 / 2)) {
        for (int yLine = yBorder + (yStaveGap * 5); yLine <= y; yLine += yStaveGap) {
          g.DrawLine(new Pen(Color.Black), x - (iNoteSizeMod * 2), yLine, x + (iNoteSizeMod * 2), yLine);
        }
      }

      // Accidental
      double scale = AccidentalScale * DrawScale;
      switch (acc) {
        case Accidental.Natural:
          g.DrawImage(bmpNatural, new Rectangle(x - (int)(NaturalWidth * scale) - (int)(AccidentalSpacing * AccidentalScale), y - (int)(NaturalTopOffset * scale), (int)(NaturalWidth * scale), (int)(NaturalHeight * scale)));
          break;
        case Accidental.Flat:
          g.DrawImage(bmpFlat, new Rectangle(x - (int)(FlatWidth * scale) - (int)(AccidentalSpacing * AccidentalScale), y - (int)(FlatTopOffset * scale), (int)(FlatWidth * scale), (int)(FlatHeight * scale)));
          break;
        case Accidental.Sharp:
          g.DrawImage(bmpSharp, new Rectangle(x - (int)(SharpWidth * scale) - (int)(AccidentalSpacing * AccidentalScale), y - (int)(SharpTopOffset * scale), (int)(SharpWidth * scale), (int)(SharpHeight * scale)));
          break;
        case Accidental.DoubleFlat: throw new NotImplementedException();
        case Accidental.DoubleSharp: throw new NotImplementedException();
        case Accidental.None: break;
      }
    }
    private void DrawClef(Graphics g, Clef clef, int xPos) {
      if (clef == Clef.Bass) {
        g.DrawImage(bmpBassClef, new Rectangle(xPos, yBorder, (int)(BassClefWidth * DrawScale), (int)(BassClefHeight * DrawScale)));
      }
      else if (clef == Clef.Treble) {
        g.DrawImage(bmpTrebleClef, new Rectangle(xPos, yBorder - (1 * yStaveGap), (int)(TrebleClefWidth * DrawScale), (int)(TrebleClefHeight * DrawScale)));
      }
      else if (clef == Clef.Treble_8) {
        g.DrawImage(bmpTreble8Clef, new Rectangle(xPos, yBorder - (1 * yStaveGap), (int)(Treble8ClefWidth * DrawScale), (int)(Treble8ClefHeight * DrawScale)));
      }
      else throw new Exception("Unknown clef : " + clef.ToString());
    }
    private void DrawKeySignature(Graphics g) {
      if (iKeySignature == 0) return;
      int iShift = (ThisClef == Clef.Bass) ? 2 : 0;
      if (iKeySignature > 0) {
        if (iKeySignature > 7) throw new Exception("Illegal key signature : " + iKeySignature);
        for (int i = 0; i < iKeySignature; i++) {
          g.DrawImage(bmpSharp, new Rectangle(AfterClefX + (AccidentalSpacing*i), yBorder + (4 * yStaveGap) - ((SharpPositions[i]-iShift) * yStaveGap / 2) - (int)(SharpTopOffset * DrawScale), (int)(SharpWidth * DrawScale), (int)(SharpHeight * DrawScale)));
        }
      }
      else {
        if (iKeySignature < -7) throw new Exception("Illegal key signature : " + iKeySignature);
        for (int i = 0; i < -iKeySignature; i++) {
          g.DrawImage(bmpFlat, new Rectangle(AfterClefX + (AccidentalSpacing * i), yBorder + (4 * yStaveGap) - ((FlatPositions[i]-iShift) * yStaveGap / 2) - (int)(FlatTopOffset * DrawScale), (int)(FlatWidth * DrawScale), (int)(FlatHeight * DrawScale)));
        }
      }
    }
    #endregion // Drawing methods

    #region Utility Methods
    private static bool IsBlackNote(int iNote) {
      int i = iNote % 12;
      if (i == 1 || i == 3 || i == 6 || i == 8 || i == 10) return true;
      return false;
    }
    private static int CalculateWhiteNoteSteps(int iNote1, int iNote2) {
      int iSteps = 0;
      if (iNote1 == iNote2) return 0;
      for (int i = Math.Min(iNote1, iNote2) + 1; i <= Math.Max(iNote1, iNote2); i++) {
        if (!IsBlackNote(i)) iSteps++;
      }
      if (iNote1 < iNote2) return iSteps;
      return -iSteps;
    }
    private static int ClefToBottomLine(Clef clef) {
      if (clef == Clef.Bass) return 43;
      if (clef == Clef.Treble) return 64;
      if (clef == Clef.Treble_8) return 52;
      throw new Exception("Unknown Clef : " + clef.ToString());
    }
    #endregion Utility Methods

    // A struct to hold a note and associated details
    private struct Note {
      public Accidental Accidental { get; set; }
      public int NotePitch { get; set; }
      public int NoteValue { get; set; }
      public int Line { get; set; }
    }
  }
}
