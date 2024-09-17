using Desktop.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop
{
    public partial class Form1 : Form
    {
        public EsemkaCompetitionEntities ent { get; set; }

        public Form1()
        {
            InitializeComponent();

            ent = new EsemkaCompetitionEntities();
        }

        PrivateFontCollection pfc = new PrivateFontCollection();

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'esemkaCompetitionDataSet.Competition' table. You can move, or remove it, as needed.
            this.competitionTableAdapter.Fill(this.esemkaCompetitionDataSet.Competition);

            try
            {
                pfc.AddFontFile("WSC2022SE_TP09_Gotham-Bold_actual_en.otf");
                pfc.AddFontFile("OpenSans-SemiBold.ttf");
                pfc.AddFontFile("OpenSans-Regular.ttf");

                this.panel1.Font = new Font(pfc.Families[2], 8.75f);
                this.label1.Font = new Font(pfc.Families[0], 9f, FontStyle.Bold);
                this.label2.Font = new Font(pfc.Families[1], 11f, FontStyle.Bold);
                this.label4.Font = new Font(pfc.Families[1], 9f, FontStyle.Bold);
                this.label6.Font = new Font(pfc.Families[1], 9f, FontStyle.Bold);

                this.BackColor = ColorTranslator.FromHtml("#bbbbbb");
                this.panel1.BackColor = ColorTranslator.FromHtml("#e51a2e");

                comboBox1_SelectedIndexChanged(null, null);
            }
            catch (Exception)
            {

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                return;
            }
            loadDataGrid();
            loadPlayoff();
        }

        void loadDataGrid()
        {
            dataGridView1.Rows.Clear();

            var data = ent.Matches.ToList().Where(x => x.CompetitionId == (int)comboBox1.SelectedValue && x.LevelId == 1).SelectMany(x => new int[]
            {
                (int)x.TeamAId,
                (int)x.TeamBId
            }).Distinct().Select(x => new
            {
                TeamName = ent.Teams.First(y => y.Id == x).TeamName,
                WDL = new Func<string>(() =>
                {
                    int w = 0, d = 0, l = 0;

                    var matches = ent.Matches
                        .ToList()
                        .Where(y => y.TeamAId == x && y.CompetitionId == (int)comboBox1.SelectedValue && y.LevelId == 1).ToList();

                    foreach (var match in matches)
                    {
                        int round_win = 0;

                        foreach (var round in match.MatchRounds)
                        {
                            if (round.ScoreTeamA > round.ScoreTeamB)
                            {
                                round_win++;
                            }
                        }

                        if (round_win == 0)
                        {
                            l++;
                        }
                        else if (round_win == 1)
                        {
                            w++;
                        }
                        else
                        {
                            d++;
                        }
                    }

                    matches = ent.Matches
                        .ToList()
                        .Where(y => y.TeamBId == x && y.CompetitionId == (int)comboBox1.SelectedValue && y.LevelId == 1).ToList();

                    foreach (var match in matches)
                    {
                        int round_win = 0;

                        foreach (var round in match.MatchRounds)
                        {
                            if (round.ScoreTeamB > round.ScoreTeamA)
                            {
                                round_win++;
                            }
                        }

                        if (round_win == 0)
                        {
                            l++;
                        }
                        else if (round_win == 1)
                        {
                            w++;
                        }
                        else
                        {
                            d++;
                        }
                    }

                    return $"{w}/{d}/{l}";
                })(),
                Point = new Func<int>(() =>
                {
                    int p = 0;

                    var matches = ent.Matches
                        .ToList()
                        .Where(y => y.TeamAId == x && y.CompetitionId == (int)comboBox1.SelectedValue && y.LevelId == 1).ToList();

                    foreach (var match in matches)
                    {
                        int round_win = 0;

                        foreach (var round in match.MatchRounds)
                        {
                            if (round.ScoreTeamA > round.ScoreTeamB)
                            {
                                round_win++;
                            }
                        }

                        if (round_win == 2)
                        {
                            p += 3;
                        }
                        else if (round_win == 1)
                        {
                            p += 1;
                        }
                    }

                    matches = ent.Matches
                        .ToList()
                        .Where(y => y.TeamBId == x && y.CompetitionId == (int)comboBox1.SelectedValue && y.LevelId == 1).ToList();

                    foreach (var match in matches)
                    {
                        int round_win = 0;

                        foreach (var round in match.MatchRounds)
                        {
                            if (round.ScoreTeamB > round.ScoreTeamA)
                            {
                                round_win++;
                            }
                        }

                        if (round_win == 2)
                        {
                            p += 3;
                        }
                        else if (round_win == 1)
                        {
                            p += 1;
                        }
                    }

                    return p;
                })(),
                APoint = new Func<int>(() =>
                {
                    int p = 0;

                    var matches = ent.Matches
                        .ToList()
                        .Where(y => y.TeamAId == x && y.CompetitionId == (int)comboBox1.SelectedValue && y.LevelId == 1).ToList();

                    foreach (var match in matches)
                    {
                        foreach (var round in match.MatchRounds)
                        {
                            p += round.ScoreTeamA;
                        }
                    }

                    matches = ent.Matches
                        .ToList()
                        .Where(y => y.TeamBId == x && y.CompetitionId == (int)comboBox1.SelectedValue && y.LevelId == 1).ToList();

                    foreach (var match in matches)
                    {
                        foreach (var round in match.MatchRounds)
                        {
                            p += round.ScoreTeamB;
                        }
                    }

                    return p;
                })()
            }).OrderByDescending(x => x.Point).ThenByDescending(x => x.APoint).ToList();

            for (int i = 0; i < data.Count; i++)
            {
                dataGridView1.Rows.Add(i + 1, data[i].TeamName, data[i].WDL, data[i].Point, data[i].APoint);

                if (data.Count < 16 && i >= 8)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
                else if (data.Count < 32 && i >= 16)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
                else if (data.Count < 64 && i >= 32)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
            }

            if (data.Count < 16)
            {
                label5.Text = $"Only 8 teams that continues to playoff";
            }
            else if (data.Count < 32)
            {
                label5.Text = $"Only 16 teams that continues to playoff";
            }
            else if (data.Count < 64)
            {
                label5.Text = $"Only 32 teams that continues to playoff";
            }
        }

        void loadPlayoff()
        {
            flowLayoutPanel1.Controls.Clear();

            var comId = (int)comboBox1.SelectedValue;

            if (comId == 1)
            {
                var uc = new Playoff8UserControl();

                var final_matches = ent.Matches.Where(x => x.CompetitionId == 1 && x.LevelId == 6).ToList();

                var semi_teams = new List<int>();

                foreach (var item in final_matches)
                {
                    var teamA = item.Team;
                    var teamB = item.Team1;

                    semi_teams.Add(teamA.Id);
                    semi_teams.Add(teamB.Id);

                    int @as = 0, bc = 0;

                    foreach (var round in item.MatchRounds)
                    {
                        if (round.ScoreTeamA > round.ScoreTeamB)
                        {
                            @as++;
                        }
                        else
                        {
                            bc++;
                        }
                    }

                    uc.label2.Text = $"{teamA.TeamName}    {@as}";
                    uc.label3.Text = $"{teamB.TeamName}    {bc}";

                    if (@as == 3)
                    {
                        uc.label2.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        uc.label1.Text = teamA.TeamName;
                        uc.label1.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                    }
                    else
                    {
                        uc.label3.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        uc.label1.Text = teamB.TeamName;
                        uc.label1.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                    }
                }

                int c = 4;

                var eli_teams = new List<int>();

                foreach (var team in semi_teams)
                {
                    var semi_matches = ent.Matches.Where(x => x.CompetitionId == 1 && x.LevelId == 5  && (
                        x.TeamAId == team || x.TeamBId == team
                    )).ToList();


                    foreach (var match in semi_matches)
                    {
                        var teamA = match.Team;
                        var teamB = match.Team1;

                        eli_teams.Add((int)teamA.Id);
                        eli_teams.Add((int)teamB.Id);

                        int @as = 0, bc = 0;

                        foreach (var round in match.MatchRounds)
                        {
                            if (round.ScoreTeamA > round.ScoreTeamB)
                            {
                                @as++;
                            }
                            else
                            {
                                bc++;
                            }
                        }

                        var uc1 = uc.Controls.Find($"label{c++}", true)[0];
                        var uc2 = uc.Controls.Find($"label{c++}", true)[0];

                        uc1.Text = $"{teamA.TeamName}    {@as}";
                        uc2.Text = $"{teamB.TeamName}    {bc}";

                        if (@as == 3)
                        {
                            uc1.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                        else
                        {
                            uc2.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                    }
                }

                foreach (var team in eli_teams)
                {
                    var semi_matches = ent.Matches.Where(x => x.CompetitionId == 1 && x.LevelId == 2 && (
                        x.TeamAId == team || x.TeamBId == team
                    )).ToList();


                    foreach (var match in semi_matches)
                    {
                        var teamA = match.Team;
                        var teamB = match.Team1;

                        int @as = 0, bc = 0;

                        foreach (var round in match.MatchRounds)
                        {
                            if (round.ScoreTeamA > round.ScoreTeamB)
                            {
                                @as++;
                            }
                            else
                            {
                                bc++;
                            }
                        }

                        var uc1 = uc.Controls.Find($"label{c++}", true)[0];
                        var uc2 = uc.Controls.Find($"label{c++}", true)[0];

                        uc1.Text = $"{teamA.TeamName}    {@as}";
                        uc2.Text = $"{teamB.TeamName}    {bc}";

                        if (@as == 2)
                        {
                            uc1.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                        else
                        {
                            uc2.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                    }
                }

                flowLayoutPanel1.Controls.Add(uc);
            }
            else if (comId == 2)
            {
                var uc = new Playoff16UserControl();

                var final_matches = ent.Matches.Where(x => x.CompetitionId == comId && x.LevelId == 6).ToList();

                var semi_teams = new List<int>();

                foreach (var item in final_matches)
                {
                    var teamA = item.Team;
                    var teamB = item.Team1;

                    semi_teams.Add(teamA.Id);
                    semi_teams.Add(teamB.Id);

                    int @as = 0, bc = 0;

                    foreach (var round in item.MatchRounds)
                    {
                        if (round.ScoreTeamA > round.ScoreTeamB)
                        {
                            @as++;
                        }
                        else
                        {
                            bc++;
                        }
                    }

                    uc.label2.Text = $"{teamA.TeamName}    {@as}";
                    uc.label3.Text = $"{teamB.TeamName}    {bc}";

                    if (@as == 3)
                    {
                        uc.label2.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        uc.label1.Text = teamA.TeamName;
                        uc.label1.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                    }
                    else
                    {
                        uc.label3.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        uc.label1.Text = teamB.TeamName;
                        uc.label1.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                    }
                }

                int c = 4;

                var quarter_teams = new List<int>();

                foreach (var team in semi_teams)
                {
                    var matches = ent.Matches.Where(x => x.CompetitionId == comId && x.LevelId == 5 && (
                        x.TeamAId == team || x.TeamBId == team
                    )).ToList();


                    foreach (var match in matches)
                    {
                        var teamA = match.Team;
                        var teamB = match.Team1;

                        quarter_teams.Add((int)teamA.Id);
                        quarter_teams.Add((int)teamB.Id);

                        int @as = 0, bc = 0;

                        foreach (var round in match.MatchRounds)
                        {
                            if (round.ScoreTeamA > round.ScoreTeamB)
                            {
                                @as++;
                            }
                            else
                            {
                                bc++;
                            }
                        }

                        var uc1 = uc.Controls.Find($"label{c++}", true)[0];
                        var uc2 = uc.Controls.Find($"label{c++}", true)[0];

                        uc1.Text = $"{teamA.TeamName}    {@as}";
                        uc2.Text = $"{teamB.TeamName}    {bc}";

                        if (@as == 3)
                        {
                            uc1.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                        else
                        {
                            uc2.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                    }
                }

                var eli_teams = new List<int>();

                foreach (var team in quarter_teams)
                {
                    var matches = ent.Matches.Where(x => x.CompetitionId == comId && x.LevelId == 4 && (
                        x.TeamAId == team || x.TeamBId == team
                    )).ToList();


                    foreach (var match in matches)
                    {
                        var teamA = match.Team;
                        var teamB = match.Team1;

                        eli_teams.Add((int)teamA.Id);
                        eli_teams.Add((int)teamB.Id);

                        int @as = 0, bc = 0;

                        foreach (var round in match.MatchRounds)
                        {
                            if (round.ScoreTeamA > round.ScoreTeamB)
                            {
                                @as++;
                            }
                            else
                            {
                                bc++;
                            }
                        }

                        var uc1 = uc.Controls.Find($"label{c++}", true)[0];
                        var uc2 = uc.Controls.Find($"label{c++}", true)[0];

                        uc1.Text = $"{teamA.TeamName}    {@as}";
                        uc2.Text = $"{teamB.TeamName}    {bc}";

                        if (@as == 3)
                        {
                            uc1.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                        else
                        {
                            uc2.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                    }
                }

                foreach (var team in eli_teams)
                {
                    var matches = ent.Matches.Where(x => x.CompetitionId == comId && x.LevelId == 2 && (
                        x.TeamAId == team || x.TeamBId == team
                    )).ToList();

                    foreach (var match in matches)
                    {
                        var teamA = match.Team;
                        var teamB = match.Team1;

                        int @as = 0, bc = 0;

                        foreach (var round in match.MatchRounds)
                        {
                            if (round.ScoreTeamA > round.ScoreTeamB)
                            {
                                @as++;
                            }
                            else
                            {
                                bc++;
                            }
                        }

                        var uc1 = uc.Controls.Find($"label{c++}", true)[0];
                        var uc2 = uc.Controls.Find($"label{c++}", true)[0];

                        uc1.Text = $"{teamA.TeamName}    {@as}";
                        uc2.Text = $"{teamB.TeamName}    {bc}";

                        if (@as == 2)
                        {
                            uc1.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                        else
                        {
                            uc2.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                    }
                }

                flowLayoutPanel1.Controls.Add(uc);
            }
            else if (comId == 3)
            {
                var uc = new Playoff32UserControl();

                var final_matches = ent.Matches.Where(x => x.CompetitionId == comId && x.LevelId == 6).ToList();

                var semi_teams = new List<int>();

                foreach (var item in final_matches)
                {
                    var teamA = item.Team;
                    var teamB = item.Team1;

                    semi_teams.Add(teamA.Id);
                    semi_teams.Add(teamB.Id);

                    int @as = 0, bc = 0;

                    foreach (var round in item.MatchRounds)
                    {
                        if (round.ScoreTeamA > round.ScoreTeamB)
                        {
                            @as++;
                        }
                        else
                        {
                            bc++;
                        }
                    }

                    uc.label2.Text = $"{teamA.TeamName}    {@as}";
                    uc.label3.Text = $"{teamB.TeamName}    {bc}";

                    if (@as == 3)
                    {
                        uc.label2.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        uc.label1.Text = teamA.TeamName;
                        uc.label1.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                    }
                    else
                    {
                        uc.label3.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        uc.label1.Text = teamB.TeamName;
                        uc.label1.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                    }
                }

                int c = 4;

                var quarter_teams = new List<int>();

                foreach (var team in semi_teams)
                {
                    var matches = ent.Matches.Where(x => x.CompetitionId == comId && x.LevelId == 5 && (
                        x.TeamAId == team || x.TeamBId == team
                    )).ToList();


                    foreach (var match in matches)
                    {
                        var teamA = match.Team;
                        var teamB = match.Team1;

                        quarter_teams.Add((int)teamA.Id);
                        quarter_teams.Add((int)teamB.Id);

                        int @as = 0, bc = 0;

                        foreach (var round in match.MatchRounds)
                        {
                            if (round.ScoreTeamA > round.ScoreTeamB)
                            {
                                @as++;
                            }
                            else
                            {
                                bc++;
                            }
                        }

                        var uc1 = uc.Controls.Find($"label{c++}", true)[0];
                        var uc2 = uc.Controls.Find($"label{c++}", true)[0];

                        uc1.Text = $"{teamA.TeamName}    {@as}";
                        uc2.Text = $"{teamB.TeamName}    {bc}";

                        if (@as == 3)
                        {
                            uc1.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                        else
                        {
                            uc2.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                    }
                }

                var eigth_teams = new List<int>();

                foreach (var team in quarter_teams)
                {
                    var matches = ent.Matches.Where(x => x.CompetitionId == comId && x.LevelId == 4 && (
                        x.TeamAId == team || x.TeamBId == team
                    )).ToList();

                    foreach (var match in matches)
                    {
                        var teamA = match.Team;
                        var teamB = match.Team1;

                        eigth_teams.Add((int)teamA.Id);
                        eigth_teams.Add((int)teamB.Id);

                        int @as = 0, bc = 0;

                        foreach (var round in match.MatchRounds)
                        {
                            if (round.ScoreTeamA > round.ScoreTeamB)
                            {
                                @as++;
                            }
                            else
                            {
                                bc++;
                            }
                        }

                        var uc1 = uc.Controls.Find($"label{c++}", true)[0];
                        var uc2 = uc.Controls.Find($"label{c++}", true)[0];

                        uc1.Text = $"{teamA.TeamName}    {@as}";
                        uc2.Text = $"{teamB.TeamName}    {bc}";

                        if (@as == 3)
                        {
                            uc1.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                        else
                        {
                            uc2.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                    }
                }

                var eli_teams = new List<int>();

                foreach (var team in eigth_teams)
                {
                    var matches = ent.Matches.Where(x => x.CompetitionId == comId && x.LevelId == 3 && (
                        x.TeamAId == team || x.TeamBId == team
                    )).ToList();

                    foreach (var match in matches)
                    {
                        var teamA = match.Team;
                        var teamB = match.Team1;

                        eli_teams.Add((int)teamA.Id);
                        eli_teams.Add((int)teamB.Id);

                        int @as = 0, bc = 0;

                        foreach (var round in match.MatchRounds)
                        {
                            if (round.ScoreTeamA > round.ScoreTeamB)
                            {
                                @as++;
                            }
                            else
                            {
                                bc++;
                            }
                        }

                        var uc1 = uc.Controls.Find($"label{c++}", true)[0];
                        var uc2 = uc.Controls.Find($"label{c++}", true)[0];

                        uc1.Text = $"{teamA.TeamName}    {@as}";
                        uc2.Text = $"{teamB.TeamName}    {bc}";

                        if (@as == 3)
                        {
                            uc1.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                        else
                        {
                            uc2.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                    }
                }

                foreach (var team in eli_teams)
                {
                    var matches = ent.Matches.Where(x => x.CompetitionId == comId && x.LevelId == 2 && (
                        x.TeamAId == team || x.TeamBId == team
                    )).ToList();

                    foreach (var match in matches)
                    {
                        var teamA = match.Team;
                        var teamB = match.Team1;

                        int @as = 0, bc = 0;

                        foreach (var round in match.MatchRounds)
                        {
                            if (round.ScoreTeamA > round.ScoreTeamB)
                            {
                                @as++;
                            }
                            else
                            {
                                bc++;
                            }
                        }

                        var uc1 = uc.Controls.Find($"label{c++}", true)[0];
                        var uc2 = uc.Controls.Find($"label{c++}", true)[0];

                        uc1.Text = $"{teamA.TeamName}    {@as}";
                        uc2.Text = $"{teamB.TeamName}    {bc}";

                        if (@as == 2)
                        {
                            uc1.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                        else
                        {
                            uc2.Font = new Font(pfc.Families[1], 8.75f, FontStyle.Bold);
                        }
                    }
                }

                flowLayoutPanel1.Controls.Add(uc);
            }
        }
    }
}
