using Microsoft.Maui.Controls.Shapes;

namespace MauiDynamicUnderlineControls.Controls;

public partial class UnderlinedEntry : Grid
{
    /////////////////////////////////////////////////////////////////////////////////
    // Members & Member Mutators
    /////////////////////////////////////////////////////////////////////////////////

    #region Border-based_Properties
    public static readonly BindableProperty EntryBackgroundColorProperty = BindableProperty.Create(nameof(EntryBackgroundColor), typeof(Color), typeof(UnderlinedEntry), new Color(0, 0, 0, 1));
    public static readonly BindableProperty EntryBackgroundProperty = BindableProperty.Create(nameof(EntryBackground), typeof(Brush), typeof(UnderlinedEntry));
    public static readonly BindableProperty StrokeProperty = BindableProperty.Create(nameof(Stroke), typeof(Brush), typeof(UnderlinedEntry));
    public static readonly BindableProperty StrokeShapeProperty = BindableProperty.Create(nameof(StrokeShape), typeof(IShape), typeof(UnderlinedEntry), new Rectangle());
    public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.Create(nameof(StrokeThickness), typeof(double), typeof(UnderlinedEntry), 0.0);
    public static readonly BindableProperty UnderlineColorProperty = BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(UnderlinedEntry), new Color(1, 0, 0));
    public static readonly BindableProperty UnderlineWidthProperty = BindableProperty.Create(nameof(UnderlineWidth), typeof(double), typeof(UnderlinedEntry), 1.0);
    public static readonly BindableProperty UnderlineStrokeThicknessProperty = BindableProperty.Create(nameof(UnderlineStrokeThickness), typeof(double), typeof(UnderlinedEntry), 0.0);
    #endregion

    #region Entry-based_Properties
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(UnderlinedEntry), default(string));
    public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(UnderlinedEntry), new Color(0.5f) );
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(UnderlinedEntry), FontAttributes.None);
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(UnderlinedEntry), default(string));
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(UnderlinedEntry), 14.0);
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(UnderlinedEntry), 0.0);
    public static readonly BindableProperty TextTransformProperty = BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(UnderlinedEntry), TextTransform.None);
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(UnderlinedEntry), default(Color));

    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(UnderlinedEntry), default(string), defaultBindingMode: BindingMode.TwoWay);

    public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(UnderlinedEntry), int.MaxValue);
    public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(UnderlinedEntry), TextAlignment.Start);
    public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(UnderlinedEntry), TextAlignment.Center);
    #endregion



    #region Border-based_Mutators

    /// <summary>
    /// Gets or sets the color of the apparent background for this entry. In truth, this actually
    /// sets the background color for the border that surrounds this entry; the entries background
    /// color is hard-coded to transparent.
    /// <br></br>
    /// This prevents the local platform from painting this color only under the areas that have text
    /// entered into the entry control.
    /// This is a bindable property.
    /// </summary>
    public Color EntryBackgroundColor
    {
        get => (Color)GetValue(EntryBackgroundColorProperty);
        set => SetValue(EntryBackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the brush of the apparent background for this entry. In truth, this actually
    /// sets the background brush for the border that surrounds this entry; the entries background
    /// is hard-coded to transparent.
    /// <br></br>
    /// This prevents the local platform from painting this brush only under the areas that have text
    /// entered into the entry control.
    /// This is a bindable property.
    /// </summary>
    [System.ComponentModel.TypeConverter(typeof(Microsoft.Maui.Controls.BrushTypeConverter))]
    public Brush EntryBackground
    {
        get =>(Brush)GetValue(EntryBackgroundProperty);
        set => SetValue(EntryBackgroundProperty, value);
    }

    /// <summary>
    /// Gets or sets the brush used to paint the stroke that surrounds this entry. This is a bindable property.
    /// </summary>
    [System.ComponentModel.TypeConverter(typeof(Microsoft.Maui.Controls.BrushTypeConverter))]
    public Brush Stroke
    {
        get => (Brush)GetValue(StrokeProperty);
        set => SetValue(StrokeProperty, value);
    }

    /// <summary>
    /// Gets or sets the shape of the stroke that surrounds this entry. This is a bindable property.
    /// </summary>
    [System.ComponentModel.TypeConverter(typeof(StrokeShapeTypeConverter))]
    public IShape StrokeShape
    {
        get => (IShape)GetValue(StrokeShapeProperty);
        set => SetValue(StrokeShapeProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the thickness for the stroke that surrounds this entry. This is a bindable property.
    /// </summary>
    public double StrokeThickness
    {
        get => (double)GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the color for the underline indicator of this entry.
    /// This is a bindable property.
    /// </summary>
    public Color UnderlineColor
    {
        get => (Color)GetValue(UnderlineColorProperty);
        set => SetValue(UnderlineColorProperty, value);
    }

    /// <summary>
    /// Gets the width of the underline indicator of this entry. This value is
    /// set programmatically, and should <em><b>not</b></em> be set manually.
    /// This is a bindable property, but should only be bound one-way.
    /// </summary>
    public double UnderlineWidth
	{
		get => (double)GetValue(UnderlineWidthProperty);
		set => SetValue(UnderlineWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the thickness for the stroke that underlines this entry, when it has focus.
    /// This is a bindable property.
    /// </summary>
    public double UnderlineStrokeThickness
    {
        get => (double)GetValue(UnderlineStrokeThicknessProperty);
        set => SetValue(UnderlineStrokeThicknessProperty, value);
    }

    #endregion

    #region Entry-based_Mutators

    /// <summary>
    /// Gets or sets the size of the placeholder text for the text of this entry.
    /// This is a bindable property.
    /// </summary>
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the placeholder text color for the text of this entry.
    /// This is a bindable property.
    /// </summary>
    public Color PlaceholderColor
    {
        get => (Color)GetValue(PlaceholderColorProperty);
        set => SetValue(PlaceholderColorProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the font for the text of this entry is bold, italic, or neither (none).
    /// This is a bindable property.
    /// </summary>
    public FontAttributes FontAttributes
    {
        get { return (FontAttributes)GetValue(FontAttributesProperty); }
        set { SetValue(FontAttributesProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font family for the text of this entry. This is a bindable property.
    /// </summary>
    public string FontFamily
    {
        get { return (string)GetValue(FontFamilyProperty); }
        set { SetValue(FontFamilyProperty, value); }
    }

    /// <summary>
    /// Gets or sets the size of the font for the text of this entry. This is a bindable property.
    /// </summary>
    [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
    public double FontSize
    {
        get { return (double)GetValue(FontSizeProperty); }
        set { SetValue(FontSizeProperty, value); }
    }
    
    /// <summary>
    /// Gets or sets the size of the character spacing for the text of this entry. This is a bindable property.
    /// </summary>
    public double CharacterSpacing
    {
        get => (double)GetValue(CharacterSpacingProperty);
        set => SetValue(CharacterSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets the text transform for the text of this entry. This is a bindable property.
    /// </summary>
    public TextTransform TextTransform
    {
        get => (TextTransform)GetValue(TextTransformProperty);
        set => SetValue(TextTransformProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the text color for the text of this entry. This is a bindable property.
    /// </summary>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the text for the text of this entry. This is a bindable property.
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Gets or sets the maximum number of characters allowed by this entry. This is a bindable property.
    /// </summary>
    public int MaxLength
    {
        get => (int)GetValue(MaxLengthProperty);
        set => SetValue(MaxLengthProperty, value);
    }

    /// <summary>
    /// Gets or sets the horizontal text alignment of this entry. This is a bindable property.
    /// </summary>
    public TextAlignment HorizontalTextAlignment
    {
        get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
        set { SetValue(HorizontalTextAlignmentProperty, value); }
    }

    /// <summary>
    /// Gets or sets the vertical text alignment of this entry. This is a bindable property.
    /// </summary>
    public TextAlignment VerticalTextAlignment
    {
        get { return (TextAlignment)GetValue(VerticalTextAlignmentProperty); }
        set { SetValue(VerticalTextAlignmentProperty, value); }
    }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    // Methods
    /////////////////////////////////////////////////////////////////////////////////
    public UnderlinedEntry()
	{
		InitializeComponent();

        ContainedEntry.Focused += ContainedEntry_Focused;
        EntryBorder.SizeChanged += EntryBorder_SizeChanged;
	}

    private void EntryBorder_SizeChanged(object sender, EventArgs e)
    {
        SetUnderlineWidth();
    }

    private void ContainedEntry_Focused(object sender, FocusEventArgs e)
    {
        SetUnderlineWidth();
    }

    private void SetUnderlineWidth()
    {
        if (EntryBorder?.StrokeShape is RoundRectangle strokeRoundRect)
        {
            double radius;
            radius = (strokeRoundRect.CornerRadius.BottomLeft > strokeRoundRect.CornerRadius.BottomRight) ? strokeRoundRect.CornerRadius.BottomLeft : strokeRoundRect.CornerRadius.BottomRight;

            UnderlineWidth = ((EntryBorder.Width - (radius * 2)) > 0) ? EntryBorder.Width - (radius * 2) : 0;
        }
        else if (EntryBorder?.StrokeShape is Rectangle strokeRect)
        {
            UnderlineWidth = strokeRect.Width;
        }
    }
}