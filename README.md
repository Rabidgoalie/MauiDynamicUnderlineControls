# Maui Dynamic Underline Controls
The controls provided in this package are a pure .NET MAUI implementation of text-based
controls that have an underline that highlights the Border that surrounds each control.
The color of the underline can be  Most bindings of the underlying controls have been
exposed and can be styled in the same way as any other XAML control, including data binding.
The only platform specific code needed is found in the 

This project came from a desire to have focus visuals for the text-based controls be unified
across the different platforms that .NET MAUI supports, while not rewriting the underlying
code to draw the controls on their native platforms (_which, I am not knowledgable enough to
do_). This led to a compromise approach: Define everything in .NET MAUI, using the controls
that are already provided, and 'glue' it all together in the XAML and code-behind.

As a result of the approach used to create these controls, they may not be as performant as
your use-case requires. There is certainly a better way of doing this...I just don't know that
way. YMMV.

# Important Project Changes
There are several key changes that must be made for these controls to
appear correctly. Without these changes, the platform specific focus visuals will
still be utilized alongside our custom focus visuals.

## C# Mappers
First, new mappers must be appended in the main, top-level, App.xaml.cs constructor (_**not** the
platform-specific App.xaml.cs files_). The code is as follows:

```
        public App()
        {
            InitializeComponent();

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
                /* WINDOWS Underline Removal
                 * 
                 * To remove the border and focus underline, the App.xaml file for
                 * the Windows Platform must be edited. See that file for the changes
                 * that need to be made.
                 */
#if ANDROID

                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);

#elif IOS || MACCATALYST

                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;

#endif
            });

            Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping(nameof(Editor), (handler, view) =>
            {
                /* WINDOWS Underline Removal
                 * 
                 * To remove the border and focus underline, the App.xaml file for
                 * the Windows Platform must be edited. See that file for the changes
                 * that need to be made.
                 */
#if ANDROID

                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);

#elif IOS || MACCATALYST

                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;

#endif
            });

            Microsoft.Maui.Handlers.SearchBarHandler.Mapper.AppendToMapping(nameof(SearchBar), (handler, view) =>
            {
                /* WINDOWS Underline Removal
                 * 
                 * To remove the border and focus underline, the App.xaml file for
                 * the Windows Platform must be edited. See that file for the changes
                 * that need to be made.
                 */
#if ANDROID

                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);

#elif IOS || MACCATALYST

                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;

#endif
            });
            
            MainPage = new AppShell();
        }
```

These new mappers will turn off the platform specific focus visuals and allow our's
to indicate focus. The code above works for iOS, MacCatalyst, and Android.
However, Windows does not use this approach (_or, not an easy to use approach like
what is seen for the other three platforms_). For Windows, we must override the WPF
XAML styling that dictates what each control looks like.

## WPF XAML
To override the Windows system focus visuals, we need to override the following
XAML resources in our App.xaml markup found **IN THE WINDOWS PLATFORM FOLDER**. This
will not work outside of this context; only this subfolder will have access to the
styling resources to be overridden. To do this, we add the following XAML markup:


```
    <!-- FINDING X:KEY VALUE DEFINITIONS
    All values can be found in 'generic.xaml', which is located (roughly):
        C:\Users\<USERNAME>\.nuget\packages\microsoft.windowsappsdk\1.2.221209.1\lib\net6.0-windows10.0.18362.0\Microsoft.WinUI\Themes
    
    Other XAML files exist that contain markup, but the file above is the
    one that was being referenced in VS2022. This was found while trying
    to create a style override:
    
        <Style TargetType="ComboBox" BasedOn="{StaticResource DefaultComboBoxStyle}">
        </Style>
    
    Then, "Peek Definition" was used on the DefaultComboBoxStyle static
    resource to find the correct 'generic.xaml' file. This might be common
    knowledge to WPF devs, but I had no idea were to look for this info.
    -->
    <maui:MauiWinUIApplication.Resources>
        <ResourceDictionary>
            <!-- IMPORTANT THEME DICTIONARY INFORMATION
            The theme values that are used from the dictionaries below
            are dictated by the user's system theme choice. Forcing MAUI
            to a different theme will NOT affect the values used below.
            
            For example, the user has set her system to the 'Light' theme
            and your App is forcing a 'Dark' theme via (in the constructor
            for App.xaml.cs):
            
                UserAppTheme = AppTheme.Dark
            
            You ARE forcing MAUI to use the dark theme, and it will respect
            this change. However, the theme used for the overridden values
            below will STILL repect the user's system theme. So, whatever
            values are set in the 'Light' theme dictionary below will be used.
            -->
            <ResourceDictionary.ThemeDictionaries>
                <ResourceDictionary x:Key="Default">
                    <!--
                    ******************************************************************
                    Custom Color/Resource Definitions
                    ******************************************************************
                    -->
                    <Color x:Key="ComboBoxDropDownGlyphFGBrush">#FFFF0000</Color>
                    <Color x:Key="ComboBoxItemSelectedBGBrush">#33FF0000</Color>
                    <Color x:Key="ComboBoxItemSelectedPointerOverBGBrush">#66FF0000</Color>



                    <!--
                    ******************************************************************
                    ComboBox Overrides
                    ******************************************************************
                    -->
                    <Thickness x:Key="CustomComboBoxBorderThemeThickness">0</Thickness>

                    <x:Int32 x:Key="CustomComboBoxPopupMaxNumberOfItems">4</x:Int32>
                    <x:Int32 x:Key="CustomComboBoxPopupMaxNumberOfItemsThatCanBeShownOnOneSide">4</x:Int32>
                    <x:Double x:Key="CustomComboBoxPopupThemeMinWidth">120</x:Double>
                    <x:Double x:Key="CustomComboBoxPopupThemeTouchMinWidth">240</x:Double>
                    <Thickness x:Key="CustomComboBoxPopupBorderThemeThickness">2</Thickness>
                    
                    
                    
                    <!--
                    ******************************************************************
                    AutoSuggestBox Overrides (SearchBar)
                    ******************************************************************
                    -->
                    <x:Double x:Key="SearchIconSize">12</x:Double>
                </ResourceDictionary>

                <ResourceDictionary x:Key="HighContrast">
                    <!--
                    ******************************************************************
                    Custom Color/Resource Definitions
                    ******************************************************************
                    -->
                    <Color x:Key="ComboBoxDropDownGlyphFGBrush">#FFFF0000</Color>
                    <Color x:Key="ComboBoxItemSelectedBGBrush">#33FF0000</Color>
                    <Color x:Key="ComboBoxItemSelectedPointerOverBGBrush">#66FF0000</Color>



                    <!--
                    ******************************************************************
                    ComboBox Overrides
                    ******************************************************************
                    -->
                    <Thickness x:Key="CustomComboBoxBorderThemeThickness">0</Thickness>

                    <x:Int32 x:Key="CustomComboBoxPopupMaxNumberOfItems">3</x:Int32>
                    <x:Int32 x:Key="CustomComboBoxPopupMaxNumberOfItemsThatCanBeShownOnOneSide">3</x:Int32>
                    <x:Double x:Key="CustomComboBoxPopupThemeMinWidth">120</x:Double>
                    <x:Double x:Key="CustomComboBoxPopupThemeTouchMinWidth">240</x:Double>
                    <Thickness x:Key="CustomComboBoxPopupBorderThemeThickness">2</Thickness>



                    <!--
                    ******************************************************************
                    AutoSuggestBox Overrides (SearchBar)
                    ******************************************************************
                    -->
                    <x:Double x:Key="SearchIconSize">20</x:Double>
                </ResourceDictionary>

                <ResourceDictionary x:Key="Light">
                    <!--
                    ******************************************************************
                    Custom Color/Resource Definitions
                    ******************************************************************
                    -->
                    <Color x:Key="ComboBoxDropDownGlyphFGBrush">#FFFFFF00</Color>
                    <Color x:Key="ComboBoxItemSelectedBGBrush">#33FFFF00</Color>
                    <Color x:Key="ComboBoxItemSelectedPointerOverBGBrush">#66FFFF00</Color>


                    <!--
                    ******************************************************************
                    ComboBox Overrides
                    ******************************************************************
                    -->
                    <Thickness x:Key="CustomComboBoxBorderThemeThickness">0</Thickness>

                    <x:Int32 x:Key="CustomComboBoxPopupMaxNumberOfItems">5</x:Int32>
                    <x:Int32 x:Key="CustomComboBoxPopupMaxNumberOfItemsThatCanBeShownOnOneSide">5</x:Int32>
                    <x:Double x:Key="CustomComboBoxPopupThemeMinWidth">120</x:Double>
                    <x:Double x:Key="CustomComboBoxPopupThemeTouchMinWidth">240</x:Double>
                    <Thickness x:Key="CustomComboBoxPopupBorderThemeThickness">2</Thickness>



                    <!--
                    ******************************************************************
                    AutoSuggestBox Overrides (SearchBar)
                    ******************************************************************
                    -->
                    <x:Double x:Key="SearchIconSize">12</x:Double>
                </ResourceDictionary>
            </ResourceDictionary.ThemeDictionaries>

            <!--
            ******************************************************************
            Entry/Editor Border Overrides (Cross-Theme Override)
            ******************************************************************
            -->
            <Thickness x:Key="TextControlBorderThemeThickness">0</Thickness>
            <Thickness x:Key="TextControlBorderThemeThicknessFocused">0</Thickness>
            <Thickness x:Key="TextControlBorderThemeThicknessUnfocused">0</Thickness>

            <!--
            ******************************************************************
            ComboBox Overrides (Picker)
            ******************************************************************
            -->
            <StaticResource x:Key="ComboBoxBorderThemeThickness" ResourceKey="CustomComboBoxBorderThemeThickness"/>

            <StaticResource x:Key="ComboBoxPopupMaxNumberOfItems" ResourceKey="CustomComboBoxPopupMaxNumberOfItems"/>
            <StaticResource x:Key="ComboBoxPopupMaxNumberOfItemsThatCanBeShownOnOneSide" ResourceKey="CustomComboBoxPopupMaxNumberOfItemsThatCanBeShownOnOneSide"/>
            <StaticResource x:Key="ComboBoxPopupThemeMinWidth" ResourceKey="CustomComboBoxPopupThemeMinWidth"/>
            <StaticResource x:Key="ComboBoxPopupThemeTouchMinWidth" ResourceKey="CustomComboBoxPopupThemeTouchMinWidth"/>
            <!--<StaticResource x:Key="ComboBoxPopupBorderThemeThickness" ResourceKey="CustomComboBoxPopupBorderThemeThickness"/>--><!--Doesn't seem to do anything-->

            <StaticResource x:Key="ComboBoxDropDownGlyphForeground" ResourceKey="ComboBoxDropDownGlyphFGBrush"/>

            <!--
            ******************************************************************
            ComboBoxItem Overrides (Picker)
            ******************************************************************
            -->
            <StaticResource x:Key="ComboBoxItemPillFillBrush" ResourceKey="ComboBoxDropDownGlyphFGBrush" />

            <!--<StaticResource x:Key="ComboBoxItemForeground" ResourceKey="SystemControlForegroundBaseHighBrush" />
            <StaticResource x:Key="ComboBoxItemForegroundPressed" ResourceKey="SystemControlHighlightAltBaseHighBrush" />
            <StaticResource x:Key="ComboBoxItemForegroundPointerOver" ResourceKey="SystemControlHighlightAltBaseHighBrush" />
            <StaticResource x:Key="ComboBoxItemForegroundDisabled" ResourceKey="SystemControlDisabledBaseMediumLowBrush" />
            <StaticResource x:Key="ComboBoxItemForegroundSelected" ResourceKey="SystemControlHighlightAltBaseHighBrush" />
            <StaticResource x:Key="ComboBoxItemForegroundSelectedUnfocused" ResourceKey="SystemControlHighlightAltBaseHighBrush" />
            <StaticResource x:Key="ComboBoxItemForegroundSelectedPressed" ResourceKey="SystemControlHighlightAltBaseHighBrush" />
            <StaticResource x:Key="ComboBoxItemForegroundSelectedPointerOver" ResourceKey="SystemControlHighlightAltBaseHighBrush" />
            <StaticResource x:Key="ComboBoxItemForegroundSelectedDisabled" ResourceKey="SystemControlDisabledBaseMediumLowBrush" />
            <StaticResource x:Key="ComboBoxItemBackground" ResourceKey="ComboBoxDropDownGlyphFGBrush" />
            <StaticResource x:Key="ComboBoxItemBackgroundPressed" ResourceKey="SystemControlHighlightListMediumBrush" />
            <StaticResource x:Key="ComboBoxItemBackgroundPointerOver" ResourceKey="SystemControlHighlightListLowBrush" />
            <StaticResource x:Key="ComboBoxItemBackgroundDisabled" ResourceKey="SystemControlTransparentBrush" />-->

            <StaticResource x:Key="ComboBoxItemBackgroundSelected" ResourceKey="ComboBoxItemSelectedBGBrush" />

            <!--<StaticResource x:Key="ComboBoxItemBackgroundSelectedUnfocused" ResourceKey="SystemControlHighlightListAccentLowBrush" />
            <StaticResource x:Key="ComboBoxItemBackgroundSelectedPressed" ResourceKey="SystemControlHighlightListAccentHighBrush" />-->

            <StaticResource x:Key="ComboBoxItemBackgroundSelectedPointerOver" ResourceKey="ComboBoxItemSelectedPointerOverBGBrush" />

            <!--<StaticResource x:Key="ComboBoxItemBackgroundSelectedDisabled" ResourceKey="SystemControlTransparentBrush" />
            <StaticResource x:Key="ComboBoxItemBorderBrush" ResourceKey="SystemControlTransparentBrush" />
            <StaticResource x:Key="ComboBoxItemBorderBrushPressed" ResourceKey="SystemControlTransparentBrush" />
            <StaticResource x:Key="ComboBoxItemBorderBrushPointerOver" ResourceKey="SystemControlTransparentBrush" />
            <StaticResource x:Key="ComboBoxItemBorderBrushDisabled" ResourceKey="SystemControlTransparentBrush" />
            <StaticResource x:Key="ComboBoxItemBorderBrushSelected" ResourceKey="SystemControlTransparentBrush" />
            <StaticResource x:Key="ComboBoxItemBorderBrushSelectedUnfocused" ResourceKey="SystemControlTransparentBrush" />
            <StaticResource x:Key="ComboBoxItemBorderBrushSelectedPressed" ResourceKey="SystemControlTransparentBrush" />
            <StaticResource x:Key="ComboBoxItemBorderBrushSelectedPointerOver" ResourceKey="SystemControlTransparentBrush" />
            <StaticResource x:Key="ComboBoxItemBorderBrushSelectedDisabled" ResourceKey="SystemControlTransparentBrush" />-->

            <!--
            ******************************************************************
            AutoSuggestBox Overrides (SearchBar)
            ******************************************************************
            -->
            <StaticResource x:Key="AutoSuggestBoxIconFontSize" ResourceKey="SearchIconSize"/>


        </ResourceDictionary>

    </maui:MauiWinUIApplication.Resources>
```


You will note that we are adding this markup in the `maui:MauiWinUIApplication.Resources` markup tag pair.
There is a fair number of static resources that are currently commented out. These resources can be uncommented
and stylized to the needs of your app.

Many of the values above are for the WPF ComboBox control. These are included only because I struggled to find
any information on overriding this control's styling to better fit the color styling for my app's needs. If you
are happy with Window's default styling for the ComboBox, these values can be omitted.

## Warning Comments in Code Examples
In the code and markup above, you will notice that there are several warning/information comments that I have left in.
I like to have these types of warnings in the code (rather than a readme like this) so that I will have this information
available within the code itself, if I ever encounter a problem that is covered by one of these warnings/information
comments. Since I always enclose these in multi-line comments, they can be closed and not clutter up the screen. These
can, of course, be removed if you don't like/want this information present.