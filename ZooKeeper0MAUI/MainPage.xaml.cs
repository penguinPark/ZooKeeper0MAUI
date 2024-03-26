﻿using Microsoft.Maui.Controls.PlatformConfiguration;

namespace ZooKeeper0MAUI;

public partial class MainPage : ContentPage
{
	static MainPage page; 
	
	public MainPage()
	{
		InitializeComponent();
		page = this; // reference to this page so we can keep our UI management stuff here
		Game.SetUpGame(page); // pass reference to static class Game that we use to manage everything
	}

	/* Method to create a button and place it on the grid
	 * whenever a new Zone is created on the board.
	 * 
	 * Called by Zone.cs
	 */


	public Button MakeGridButton(int x, int y)
	{
		Button theButton = new Button
		{
			WidthRequest = 80,
			HeightRequest = 80,
			BorderColor = Colors.Black,
			BorderWidth = 1,
			BackgroundColor = Colors.White,
			LineBreakMode = LineBreakMode.CharacterWrap,
			FontFamily = "NotoEmoji-Regular"
		};
        theButton.Clicked += ZoneButton_Clicked;
        page.ZooGrid.Add(theButton, x, y);
        return theButton;
	}


    public void ZoneButton_Clicked(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		int c = ZooGrid.GetColumn(button);
		int r = ZooGrid.GetRow(button);
        Game.ZoneClick(Game.animalZones[r][c]);
	}


    /* Event handlers for buttons. Notice that these basically
	 * pass on a request to the static Game class and get back
	 * whether or not it succeeded. The success or failure in turn
	 * controls whether or not the button stays enabled.
	 * 
	 * Technically this is not ideal, since the user has to click
	 * the button once and have it fail BEFORE the button grays out.
	 * Can you improve on this?
	 */
	/*
    void Down_Button_Clicked(System.Object sender, System.EventArgs e)
    {
		if (!Game.AddZones(Direction.down))
		{
			DownButton.IsEnabled = false;
			DownButton.BackgroundColor = Colors.LightGray;
		}
    }

    void Right_Button_Clicked(System.Object sender, System.EventArgs e)
    {
        if (!Game.AddZones(Direction.right))
        {
            RightButton.IsEnabled = false;
            RightButton.BackgroundColor = Colors.LightGray;
        }
    }*/

	/* In this simple version, each kind of animal has a custom click handler for being added to the holding pen if it comes from the "add" buttons, yet animals can be put back into the holding pen from the "zoo" grid without needing custom code for each animal type.
	 * 
	 * Can you make similar improvements to the XAML and backing code so that the animal-generating buttons (the "add" buttons)
	 *
	 */

    void Cat_Button_Clicked(System.Object sender, System.EventArgs e)
    {
		Game.AddAnimalToHolding("cat");
    }

	// "Mouse" here is the animal, not the button on your mouse!
    void Mouse_Button_Clicked(System.Object sender, System.EventArgs e)
    {
        Game.AddAnimalToHolding("mouse");
    }

	void HoldingPen_Clicked(object sender, EventArgs e)
    {
		//Game.ZoneClick()
    }

    void Raptor_Button_Clicked(object sender, EventArgs e)
    {
        Game.AddAnimalToHolding("raptor");
    }

    void Chick_Button_Clicked(object sender, EventArgs e)
    {
        Game.AddAnimalToHolding("chick");
    }

    private void Grass_Button_Clicked(object sender, EventArgs e)
    {
        Game.AddAnimalToHolding("grass");
    }

    private void Rooster_Button_Clicked(object sender, EventArgs e)
    {
        Game.AddAnimalToHolding("rooster");
    }

    private void Vulture_Button_Clicked(object sender, EventArgs e)
    {
        Game.AddAnimalToHolding("vulture");
    }

    private void Corpse_Button_Clicked(object sender, EventArgs e)
    {
        Game.AddAnimalToHolding("corpse");
    }
}


