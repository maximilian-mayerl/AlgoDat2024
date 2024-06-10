# Foliensatz 7: Collision Detection

## Übung 1: Rectangular Bounding Volume

```csharp
class Rectangle {
    public float X { get; set; }
    public float Y { get; set; }

    public float Width { get; set; }
    public float Height { get; set; }

    public Rectangle(float x, float y, float width, float height) {
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
    }

    public bool OverlapsWith(Rectangle? other) {
        // If other is null, it can't overlap.
        if (other == null) {
            return false;
        }

        // There are four cases in which two rectangles do not overlap.
        // (1) The left edge of one is right of the right edge of the other.
        if (other.X > this.X + this.Width) {
            return false;
        }
        // (2) The right edge of one is left of the left edge of the other.
        if (other.X + other.Width < this.X) {
            return false;
        }
        // (3) The top edge of one is below the bottom edge of the other.
        if (other.Y > this.Y + this.Height) {
            return false;
        }
        // (4) The bottom edge of one is above the top edge of the other.
        if (other.Y + other.Height < this.Y) {
            return false;
        }

        // If we did not find a contradiction,
        // the rectangles must overlap.
        return true;
    }
}
```
