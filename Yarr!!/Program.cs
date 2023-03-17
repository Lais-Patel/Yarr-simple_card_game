using System;
using System.Xml.Linq;

public class CardGame
{
    private Yarr_Card[] cards = new Yarr_Card[5];
    public string[] suits_names = { "moon", "sun", "rose", "star" };
    public Random rand = new Random();
    private string locked_cards_choice;
    private char lock_1 = '1';
    private char lock_0 = '0';
    private int console_line_location_scoreboard;
    private int scoreboard_hand_to_save_to;

    public class Yarr_Card
    {
        public int position;
        public string suit;
        public int value;
        public bool locked;
        public int console_line_location_card;
    }


    void roll_new_cards()
    {
        for (int i = 0; i < 5; i++)
        {
            if (cards[i].locked == false)
            {
                cards[i].locked = false;
                cards[i].position = i + 1;
                cards[i].suit = suits_names[rand.Next(0, 4)];
                cards[i].value = rand.Next(1, 7);
            }
        }
    }

    void initialise()
    {
        Console.WriteLine();
        Console.WriteLine("Welcome to Yarr!");
        Console.WriteLine("This is a simple card CardGame about adding numbers");
        Console.WriteLine("And correlating suits to get a higher score");
        Console.WriteLine("Use '1' for locked and '0' for unlocked");
        Console.WriteLine("Example: '01001' would lock the 2nd and 5th card");

        Console.WriteLine();
        console_line_location_scoreboard = Console.CursorTop;
        Console.WriteLine("Oney Ones      - ");
        Console.WriteLine("Twoey Twos     - ");
        Console.WriteLine("Threey Threes  - ");
        Console.WriteLine("Foury Fours    - ");
        Console.WriteLine("Fivey Fives    - ");
        Console.WriteLine("Sixy Sixes     - ");
        Console.WriteLine();

        for (int i = 0; i < 5; i++)
        {
            cards[i] = new Yarr_Card();
            cards[i].locked = false;
            cards[i].console_line_location_card = Console.CursorTop;
            Console.WriteLine("Pos " + cards[i].position + ": " + cards[i].value + " " + cards[i].suit);
        }
    }

    void refresh_cards()
    {
        for (int i = 0; i < 5; i++)
        {
            Console.SetCursorPosition(0, cards[i].console_line_location_card);
            Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
            Console.WriteLine("Pos " + cards[i].position + ": " + cards[i].value + " " + cards[i].suit);
        }
    }

    void locked_choice_prompt()
    {
        Console.WriteLine();
        Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
        Console.Write("Which cards do you want to lock - ");
        locked_cards_choice = Console.ReadLine();
        Console.SetCursorPosition(0, Console.CursorTop-1);
        Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
        Console.WriteLine();
        for (int i = 0; i < 5; i++)
        {
            if (locked_cards_choice[i] == lock_1)
            {
                cards[i].locked = true;
            }
            else if (locked_cards_choice[i] == lock_0)
            {
                cards[i].locked = false;
            }
        }
    }

    void reroll_stage()
    {
        for (int i = 0; i < 3; i++)
        {
            this.roll_new_cards();
            this.refresh_cards();
            if (i != 2)
            {
                this.locked_choice_prompt();
            }
        }
    }

    void update_scoreboard()
    {
        Console.WriteLine();
        Console.SetCursorPosition(0, console_line_location_scoreboard);
        Console.WriteLine("Oney Ones      - ");
        Console.WriteLine("Twoey Twos     - ");
        Console.WriteLine("Threey Threes  - ");
        Console.WriteLine("Foury Fours    - ");
        Console.WriteLine("Fivey Fives    - ");
        Console.WriteLine("Sixy Sixes     - ");
        Console.WriteLine();

    }

    void save_hand_to_scoreboard()
    {   
        Console.WriteLine();
        Console.Write("Where do you want to save the sum on this hand to? - ");
        scoreboard_hand_to_save_to = int.Parse(Console.ReadLine());
        int temp_count = 0;
        for (int i = 0;i < 5; i++)
        {
            if (cards[i].value == scoreboard_hand_to_save_to)
            {
                temp_count += cards[i].value;
            }
        }
        Console.SetCursorPosition(17, console_line_location_scoreboard+scoreboard_hand_to_save_to-1);
        Console.Write(temp_count);
    }
    
    void play_game()
    {
        //Initialise the Yarr card objects
        //Send introductory tutorial of game
        initialise();

        for (int j = 0; j < 6; j++)
        {
            //Refresh cards
            for (int i = 0; i < 5; i++)
            { 
                cards[i].locked = false;
                cards[i].position = i + 1;
                cards[i].suit = suits_names[rand.Next(0, 4)];
                cards[i].value = rand.Next(1, 7);
            }
            //Update Hands scoreboard
            update_scoreboard();
            //Rerolling stage of the CardGame
            reroll_stage();
            //Save total to Match 1-6
            save_hand_to_scoreboard();
            //Opponents turn
        }
    }

    static void Main(string[] args)
    {
        CardGame game = new CardGame();
        game.play_game();
        Console.ReadLine();
    }
}