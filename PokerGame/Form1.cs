using Microsoft.VisualBasic.ApplicationServices;
using System.Collections;
using System.Text;

namespace PokerGame
{
    public partial class Form1 : Form
    {
        bool showCardsInOrder = false;
        bool autoPlay = false;
        int noMadeHand;
        int onePair;
        int twoPair;
        int threeOfAKindInt;
        int straights;
        int flushes;
        int fullHouse;
        int fourOfAKindInt;
        int straightFlushes;
        int royalFlushes;

        private int[][] RoyalFlushStraightCombination = new int[][] { new int[] { 10, 11, 12, 13, 1 } };
        private int[][] StraightCombinations = new int[][] { new int[] { 1, 2, 3, 4, 5 }, new int[] { 2, 3, 4, 5, 6 },
            new int[] {3,4,5,6,7}, new int[]{4,5,6,7,8 }, new int[]{5,6,7,8,9 }, new int[]{6,7,8,9,10 },
            new int[]{7,8,9,10,11 }, new int[]{8,9,10,11,12 }, new int[]{9,10,11,12,13 } };


        private Card[] cards = new Card[5];
        private PictureBox[] PictureBoxes;
        int balance;
        int bet;

        
        public Form1()
        {
            InitializeComponent();
        }

        private void checkHand()
        {
            balance = Convert.ToInt32(lblBalance.Text);

            string checkIfEmptyBet = betTextbox.Text;//check if bet is empty
            if (checkIfEmptyBet == "") { }
            else
            {
                bet = Convert.ToInt32(betTextbox.Text);
            }
            bool forNowOk = true;
            bool flush = true;

            for (int j = 1; j < cards.Length; j++) {
                if (cards[j - 1].Value != cards[j].Value - 1) //is there a straight?
                {
                    forNowOk = false;
                    break;
                }
            }

            for (int j = 1; j < cards.Length; j++) //flush check
            {
                if (cards[j - 1].Suit != cards[j].Suit)
                {
                    flush = false;
                    break;
                }
            }
            if (flush && forNowOk) { //straight flush
                if (cards[0].Value == 10 && cards[1].Value == 11 && cards[2].Value == 12 && cards[3].Value == 13 && cards[4].Value == 14)
                {
                    label1.Text = "Royal Flush";
                    MessageBox.Show("Congrats! You just got a Royal Flush (350x payout)!");
                    balance = bet * 350 + balance;
                    lblBalance.Text = Convert.ToString(balance);
                    timer1.Stop();

                    royalFlushes = Convert.ToInt32(lblRoyalFlushCount.Text);
                    royalFlushes++;
                    lblRoyalFlushCount.Text = Convert.ToString(royalFlushes);
                    return;
                }
                else
                {
                    label1.Text = "Straight Flush";
                    balance = bet * 50 + balance;
                    lblBalance.Text = Convert.ToString(balance);

                    straightFlushes = Convert.ToInt32(lblStraightFlushCount.Text);
                    straightFlushes++;
                    lblStraightFlushCount.Text = Convert.ToString(straightFlushes);
                    return;
                }
            }

            if (flush && forNowOk == false) //flush
            {
                label1.Text = "Flush";
                balance = bet * 6 + balance;
                lblBalance.Text = Convert.ToString(balance);

                flushes = Convert.ToInt32(lblFlushCount.Text);
                flushes++;
                lblFlushCount.Text = Convert.ToString(flushes);
                return;
            }
            if (forNowOk && flush == false) //straight
            {
                label1.Text = "Straight";
                balance = bet * 4 + balance;
                lblBalance.Text = Convert.ToString(balance);

                straights = Convert.ToInt32(lblStraightCount.Text);
                straights++;
                lblStraightCount.Text = Convert.ToString(straights);
                return;
            }

            int[] countedCardValue = new int[15];
            for (int i = 0; i < cards.Length; i++)
            {
                countedCardValue[cards[i].Value]++;
            }

            bool threeOfAKind = false;
            int twoOfAKind = 0;
            bool fourOfAKind = false;

            for (int i = 2; i < countedCardValue.Length; i++)
            {
                if (countedCardValue[i] == 2)
                {
                    twoOfAKind++;
                }
                else if (countedCardValue[i] == 3)
                {
                    threeOfAKind = true;
                }
                else if (countedCardValue[i] == 4)
                {
                    fourOfAKind = true;
                }

            }

            if (fourOfAKind == true) //4 of a kind
            {
                label1.Text = "Four of a Kind!";
                balance = bet * 25 + balance;
                lblBalance.Text = Convert.ToString(balance);

                fourOfAKindInt = Convert.ToInt32(lbl4ofAKindCount.Text);
                fourOfAKindInt++;
                lbl4ofAKindCount.Text = Convert.ToString(fourOfAKindInt);
            }

            if (threeOfAKind == true && twoOfAKind == 1)
            {
                label1.Text = "Full House";
                balance = bet * 9 + balance;
                lblBalance.Text = Convert.ToString(balance);

                fullHouse = Convert.ToInt32(lblFullHouseCount.Text);
                fullHouse++;
                lblFullHouseCount.Text = Convert.ToString(fullHouse);
            }
            else if(threeOfAKind == true && twoOfAKind == 0)
            {
                label1.Text = "Three of a Kind";
                balance = bet * 3 + balance;
                lblBalance.Text = Convert.ToString(balance);

                threeOfAKindInt = Convert.ToInt32(lbl3ofAKindCount.Text);
                threeOfAKindInt++;
                lbl3ofAKindCount.Text = Convert.ToString(threeOfAKindInt);
            }

            if(twoOfAKind == 1) //Two of a Kind should only pay Jacks or higher
            {
                label1.Text = "One Pair";
                lblBalance.Text = Convert.ToString(balance);

                onePair = Convert.ToInt32(lblOnePairCount.Text);
                onePair++;
                lblOnePairCount.Text = Convert.ToString(onePair);
            }

            if(twoOfAKind == 2)//two pair
            {
                label1.Text = "Two Pair";
                balance = bet * 2 + balance;
                lblBalance.Text = Convert.ToString(balance);

                twoPair = Convert.ToInt32(lblTwoPairCount.Text);
                twoPair++;
                lblTwoPairCount.Text = Convert.ToString(twoPair);
            }

            

            if (twoOfAKind == 0 && forNowOk == false && threeOfAKind == false) //no hand made
            {
                label1.Text = "You Lost";
                balance = balance - bet;
                lblBalance.Text = Convert.ToString(balance);

                noMadeHand = Convert.ToInt32(lblNoHandCount.Text);
                noMadeHand++; //count
                lblNoHandCount.Text = Convert.ToString(noMadeHand);
            }

            int handsPlayed = Convert.ToInt32(lblHandsPlayed.Text);
            handsPlayed++;
            lblHandsPlayed.Text = Convert.ToString(handsPlayed);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PictureBoxes = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5 };
            balance = Convert.ToInt32(lblBalance.Text);
        }

        private void btnMaxBet_Click(object sender, EventArgs e)
        {
            int maxBet = balance/10;
            betTextbox.Text = Convert.ToString(maxBet);
        }

        private void button1_Click(object sender, EventArgs e) //button click
        {
            
                balance = Convert.ToInt32(lblBalance.Text); //input balance
                string checkIfEmptyBet = betTextbox.Text;
                if (checkIfEmptyBet == "") { }
                else
                {
                    bet = Convert.ToInt32(betTextbox.Text); //input bet
                    int maxbet = balance / 10; //definition max bet
                    if (bet > maxbet) //if bet is too big it will be removed and set to the maximum allowed
                    {
                        bet = maxbet;
                        betTextbox.Text = Convert.ToString(bet);
                    }
                }
                Random rnd = new Random();


                for (int i = 0; i < 5; i++)
                {
                    Card cardValue = new Card();
                    cards[i] = cardValue;
                    int randomCard = rnd.Next(2, 15);

                    int randomCardSuit = rnd.Next(1, 5);

                    bool found = true;
                    while (found)
                    {
                        found = false;
                        for (int j = 0; j < i; j++)
                        {

                            if (cards[j].Value == randomCard && cards[j].Suit == randomCardSuit)
                            {
                                randomCard = rnd.Next(2, 15);
                                randomCardSuit = rnd.Next(1, 5);

                                found = true;
                            }
                        }
                    }

                    cards[i].Value = randomCard;
                    cards[i].Suit = randomCardSuit;

                    if (showCardsInOrder == false)
                    {
                        PictureBoxes[i].Image = Image.FromFile(cards[i].ImageName);
                    }

                }

                cards = cards.OrderBy(cv => cv.Value).ToArray();

                if (showCardsInOrder == true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        PictureBoxes[i].Image = Image.FromFile(cards[i].ImageName);
                    }
                }
                checkHand();
            
        }


        private void autoPlayFunction() //AUTOPLAY
        {           

                balance = Convert.ToInt32(lblBalance.Text); //input balance
                string checkIfEmptyBet = betTextbox.Text;
                if (checkIfEmptyBet == "") { }
                else
                {
                    bet = Convert.ToInt32(betTextbox.Text); //input bet
                    int maxbet = balance / 10; //definition max bet
                    if (bet > maxbet) //if bet is too big it will be removed and set to the maximum allowed
                    {
                        bet = maxbet;
                        betTextbox.Text = Convert.ToString(bet);
                    }
                }
                Random rnd = new Random();


                for (int i = 0; i < 5; i++)
                {
                    Card cardValue = new Card();
                    cards[i] = cardValue;
                    int randomCard = rnd.Next(2, 15);

                    int randomCardSuit = rnd.Next(1, 5);

                    bool found = true;
                    while (found)
                    {
                        found = false;
                        for (int j = 0; j < i; j++)
                        {

                            if (cards[j].Value == randomCard && cards[j].Suit == randomCardSuit)
                            {
                                randomCard = rnd.Next(2, 15);
                                randomCardSuit = rnd.Next(1, 5);

                                found = true;
                            }
                        }
                    }

                    cards[i].Value = randomCard;
                    cards[i].Suit = randomCardSuit;

                    if (showCardsInOrder == false)
                    {
                        PictureBoxes[i].Image = Image.FromFile(cards[i].ImageName);
                    }

                }

                cards = cards.OrderBy(cv => cv.Value).ToArray();

                if (showCardsInOrder == true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        PictureBoxes[i].Image = Image.FromFile(cards[i].ImageName);
                    }
                }
                checkHand();
               
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void sortCardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(sortCardsToolStripMenuItem.Text == "Sort Cards") { 
                showCardsInOrder = true;
                sortCardsToolStripMenuItem.Text = "Don't Sort cards";
            }
            else
            {
                sortCardsToolStripMenuItem.Text = "Sort Cards";
                showCardsInOrder = false;
            }
        }

        private void autoPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoPlay = !autoPlay;
            if (autoPlay)
            {
                startToolStripMenuItem.Text = "Stop";
                timer1.Interval = 1;
                timer1.Enabled = true;
            }
            if (autoPlay == false)
            {
                startToolStripMenuItem.Text = "Start";
                timer1.Stop();
            }
        }

        private void delayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            autoPlayFunction();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made By Vuk Ilic");
        }
    }
}