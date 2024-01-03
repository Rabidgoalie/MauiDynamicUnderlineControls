#nullable disable
using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;

namespace MauiDynamicUnderlineControls.Controls;

public partial class UnderlinedSearchBar : Grid
{
    /////////////////////////////////////////////////////////////////////////////////
    // Members & Member Mutators
    /////////////////////////////////////////////////////////////////////////////////

    #region Border-based_Properties
    public static readonly BindableProperty SearchBarBackgroundColorProperty = BindableProperty.Create(nameof(SearchBarBackgroundColor), typeof(Color), typeof(UnderlinedSearchBar), new Color(0, 0, 0, 1));
    public static readonly BindableProperty SearchBarBackgroundProperty = BindableProperty.Create(nameof(SearchBarBackground), typeof(Brush), typeof(UnderlinedSearchBar));
    public static readonly BindableProperty StrokeProperty = BindableProperty.Create(nameof(Stroke), typeof(Brush), typeof(UnderlinedSearchBar));
    public static readonly BindableProperty StrokeShapeProperty = BindableProperty.Create(nameof(StrokeShape), typeof(IShape), typeof(UnderlinedSearchBar), new Rectangle());
    public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.Create(nameof(StrokeThickness), typeof(double), typeof(UnderlinedSearchBar), 0.0);
    public static readonly BindableProperty UnderlineColorProperty = BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(UnderlinedSearchBar), new Color(1, 0, 0));
    public static readonly BindableProperty UnderlineWidthProperty = BindableProperty.Create(nameof(UnderlineWidth), typeof(double), typeof(UnderlinedSearchBar), 1.0);
    public static readonly BindableProperty UnderlineStrokeThicknessProperty = BindableProperty.Create(nameof(UnderlineStrokeThickness), typeof(double), typeof(UnderlinedSearchBar), 0.0);
    #endregion

    #region SearchBar-based_Properties
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(UnderlinedSearchBar), default(string));
    public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(UnderlinedSearchBar), new Color(0.5f));
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(UnderlinedSearchBar), FontAttributes.None);
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(UnderlinedSearchBar), default(string));
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(UnderlinedSearchBar), 14.0);
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(UnderlinedSearchBar), 0.0);
    public static readonly BindableProperty TextTransformProperty = BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(UnderlinedSearchBar), TextTransform.None);
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(UnderlinedSearchBar), default(Color));

    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(UnderlinedSearchBar), defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) => ((UnderlinedSearchBar)bindable).OnTextChanged((string)oldValue, (string)newValue));

    public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(UnderlinedSearchBar), int.MaxValue);
    public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(UnderlinedSearchBar), TextAlignment.Start);
    public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(UnderlinedSearchBar), TextAlignment.Center);

    /// <summary>Bindable property for <see cref="SearchCommand"/>.</summary>
    public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create(nameof(SearchCommand), typeof(ICommand), typeof(UnderlinedSearchBar), null);

    /// <summary>Bindable property for <see cref="SearchCommandParameter"/>.</summary>
    public static readonly BindableProperty SearchCommandParameterProperty = BindableProperty.Create(nameof(SearchCommandParameter), typeof(object), typeof(UnderlinedSearchBar), null);

    /// <summary>Bindable property for <see cref="CancelButtonColor"/>.</summary>
    public static readonly BindableProperty CancelButtonColorProperty = BindableProperty.Create(nameof(CancelButtonColor), typeof(Color), typeof(UnderlinedSearchBar), default(Color));
    #endregion



    #region Border-based_Mutators

    /// <summary>
    /// Gets or sets the color of the apparent background for this search bar. In truth, this actually
    /// sets the background color for the border that surrounds this search bar; the search bar's background
    /// color is hard-coded to transparent.
    /// <br></br>
    /// This prevents the local platform from painting this color only under the areas that have text
    /// entered into the search bar control.
    /// This is a bindable property.
    /// </summary>
    public Color SearchBarBackgroundColor
    {
        get => (Color)GetValue(SearchBarBackgroundColorProperty);
        set => SetValue(SearchBarBackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the brush of the apparent background for this search bar. In truth, this actually
    /// sets the background brush for the border that surrounds this search bar; the search bar's background
    /// is hard-coded to transparent.
    /// <br></br>
    /// This prevents the local platform from painting this brush only under the areas that have text
    /// entered into the search bar control.
    /// This is a bindable property.
    /// </summary>
    [System.ComponentModel.TypeConverter(typeof(Microsoft.Maui.Controls.BrushTypeConverter))]
    public Brush SearchBarBackground
    {
        get => (Brush)GetValue(SearchBarBackgroundProperty);
        set => SetValue(SearchBarBackgroundProperty, value);
    }

    /// <summary>
    /// Gets or sets the brush used to paint the stroke that surrounds this search bar. This is a bindable property.
    /// </summary>
    [System.ComponentModel.TypeConverter(typeof(Microsoft.Maui.Controls.BrushTypeConverter))]
    public Brush Stroke
    {
        get => (Brush)GetValue(StrokeProperty);
        set => SetValue(StrokeProperty, value);
    }

    /// <summary>
    /// Gets or sets the shape of the stroke that surrounds this search bar. This is a bindable property.
    /// </summary>
    [System.ComponentModel.TypeConverter(typeof(StrokeShapeTypeConverter))]
    public IShape StrokeShape
    {
        get => (IShape)GetValue(StrokeShapeProperty);
        set => SetValue(StrokeShapeProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the thickness for the stroke that surrounds this search bar. This is a bindable property.
    /// </summary>
    public double StrokeThickness
    {
        get => (double)GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the color for the underline indicator of this search bar.
    /// This is a bindable property.
    /// </summary>
    public Color UnderlineColor
    {
        get => (Color)GetValue(UnderlineColorProperty);
        set => SetValue(UnderlineColorProperty, value);
    }

    /// <summary>
    /// Gets the width of the underline indicator of this search bar. This value is
    /// set programmatically, and should <em><b>not</b></em> be set manually.
    /// This is a bindable property, but should only be bound one-way.
    /// </summary>
    public double UnderlineWidth
    {
        get => (double)GetValue(UnderlineWidthProperty);
        set => SetValue(UnderlineWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the thickness for the stroke that underlines this search bar, when it has focus.
    /// This is a bindable property.
    /// </summary>
    public double UnderlineStrokeThickness
    {
        get => (double)GetValue(UnderlineStrokeThicknessProperty);
        set => SetValue(UnderlineStrokeThicknessProperty, value);
    }

    #endregion

    #region SearchBar-based_Mutators

    /// <summary>
    /// Gets or sets the size of the placeholder text for the text of this search bar.
    /// This is a bindable property.
    /// </summary>
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the placeholder text color for the text of this search bar.
    /// This is a bindable property.
    /// </summary>
    public Color PlaceholderColor
    {
        get => (Color)GetValue(PlaceholderColorProperty);
        set => SetValue(PlaceholderColorProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the font for the text of this search bar is bold, italic, or neither (none).
    /// This is a bindable property.
    /// </summary>
    public FontAttributes FontAttributes
    {
        get { return (FontAttributes)GetValue(FontAttributesProperty); }
        set { SetValue(FontAttributesProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font family for the text of this search bar. This is a bindable property.
    /// </summary>
    public string FontFamily
    {
        get { return (string)GetValue(FontFamilyProperty); }
        set { SetValue(FontFamilyProperty, value); }
    }

    /// <summary>
    /// Gets or sets the size of the font for the text of this search bar. This is a bindable property.
    /// </summary>
    [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
    public double FontSize
    {
        get { return (double)GetValue(FontSizeProperty); }
        set { SetValue(FontSizeProperty, value); }
    }

    /// <summary>
    /// Gets or sets the size of the character spacing for the text of this search bar. This is a bindable property.
    /// </summary>
    public double CharacterSpacing
    {
        get => (double)GetValue(CharacterSpacingProperty);
        set => SetValue(CharacterSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets the text transform for the text of this search bar. This is a bindable property.
    /// </summary>
    public TextTransform TextTransform
    {
        get => (TextTransform)GetValue(TextTransformProperty);
        set => SetValue(TextTransformProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the text color for the text of this search bar. This is a bindable property.
    /// </summary>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the text for the text of this search bar. This is a bindable property.
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Gets or sets the maximum number of characters allowed by this search bar. This is a bindable property.
    /// </summary>
    public int MaxLength
    {
        get => (int)GetValue(MaxLengthProperty);
        set => SetValue(MaxLengthProperty, value);
    }

    /// <summary>
    /// Gets or sets the horizontal text alignment of this search bar. This is a bindable property.
    /// </summary>
    public TextAlignment HorizontalTextAlignment
    {
        get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
        set { SetValue(HorizontalTextAlignmentProperty, value); }
    }

    /// <summary>
    /// Gets or sets the vertical text alignment of this search bar. This is a bindable property.
    /// </summary>
    public TextAlignment VerticalTextAlignment
    {
        get { return (TextAlignment)GetValue(VerticalTextAlignmentProperty); }
        set { SetValue(VerticalTextAlignmentProperty, value); }
    }

    public ICommand SearchCommand
    {
        get { return (ICommand)GetValue(SearchCommandProperty); }
        set { SetValue(SearchCommandProperty, value); }
    }

    public object SearchCommandParameter
    {
        get { return GetValue(SearchCommandParameterProperty); }
        set { SetValue(SearchCommandParameterProperty, value); }
    }

    public Color CancelButtonColor
    {
        get { return (Color)GetValue(CancelButtonColorProperty); }
        set { SetValue(CancelButtonColorProperty, value); }
    }

    #endregion



    public event EventHandler<TextChangedEventArgs> TextChanged;



    /////////////////////////////////////////////////////////////////////////////////
    // Methods
    /////////////////////////////////////////////////////////////////////////////////
    public UnderlinedSearchBar()
	{
		InitializeComponent();

        ContainedSearchBar.Focused += ContainedSearchBar_Focused;
        SearchBarBorder.SizeChanged += SearchBarBorder_SizeChanged;
    }

    private void SearchBarBorder_SizeChanged(object sender, EventArgs e)
    {
        SetUnderlineWidth();
    }

    private void ContainedSearchBar_Focused(object sender, FocusEventArgs e)
    {
        SetUnderlineWidth();
    }

    protected virtual void OnTextChanged(string oldValue, string newValue)
    {
        TextChanged?.Invoke(this, new TextChangedEventArgs(oldValue, newValue));
    }

    private void SetUnderlineWidth()
    {
        if (SearchBarBorder?.StrokeShape is RoundRectangle strokeRoundRect)
        {
            double radius;
            radius = (strokeRoundRect.CornerRadius.BottomLeft > strokeRoundRect.CornerRadius.BottomRight) ? strokeRoundRect.CornerRadius.BottomLeft : strokeRoundRect.CornerRadius.BottomRight;

            UnderlineWidth = ((SearchBarBorder.Width - (radius * 2)) > 0) ? SearchBarBorder.Width - (radius * 2) : 0 ;
        }
        else if (SearchBarBorder?.StrokeShape is Rectangle strokeRect)
        {
            UnderlineWidth = strokeRect.Width;
        }
    }
}