using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace DungeonSlime;

public class Game1 : Core
{
    // Defines the slime sprite
    private AnimatedSprite _slime;

    // Defines the bat sprite
    private AnimatedSprite _bat;

    // Tracks the position of the slime.
    private Vector2 _slimePosition;

    // Speed multiplier when moving.
    private const float MOVEMENT_SPEED = 5.0f;

    public Game1() : base("Dungeon Slime", 1280, 720, false) { }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // Create the texture atlas from the XML configuration file
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");

        // Create the slime animated sprite from the atlas
        _slime = atlas.CreateAnimatedSprite("slime-animation");
        _slime.Scale = new Vector2(4.0f, 4.0f);

        // Create the bat animated sprite from the atlas
        _bat = atlas.CreateAnimatedSprite("bat-animation");
        _bat.Scale = new Vector2(4.0f, 4.0f);
    }

    protected override void Update(GameTime gameTime)
    {
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Update the slime animated sprite.
        _slime.Update(gameTime);

        // Update the bat animated sprite.
        _bat.Update(gameTime);
        
        // Check for mouse input and handle it.
        CheckMouseInput();

        base.Update(gameTime);
    }

    private void CheckMouseInput()
    {
        // Get the current state of mouse input.
        MouseState mouseState = Mouse.GetState();

        // If the middle mouse button is held down, the movement speed increases by 1.5
        float speed = MOVEMENT_SPEED;
        if (mouseState.MiddleButton == ButtonState.Pressed)
        {
            speed *= 1.5f;
        }
        
        // Check if the left mouse button is pressed down.
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            // Move the slime up on the screen.
            _slimePosition.Y -= speed;
            // Move the slime left on the screen.
            _slimePosition.X -= speed;
        }

        // Check if the right mouse button is pressed down.
        if (mouseState.RightButton == ButtonState.Pressed)
        {
            // Move the slime down on the screen.
            _slimePosition.Y += speed;
            // Move the slime right on the screen.
            _slimePosition.X += speed;
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        // Clear the back buffer
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Begin the sprite batch to prepare for rendering.
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        // Draw the slime sprite
        _slime.Draw(SpriteBatch, _slimePosition);

        // Draw the bat sprite 10px to the right of the slime
        _bat.Draw(SpriteBatch, new Vector2(_slime.Width + 10, 0));

        //Always end the sprite batch when finished.
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
