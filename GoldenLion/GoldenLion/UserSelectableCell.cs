using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GoldenLion
{
    public class UserSelectableCell : ViewCell
    {
        public static readonly BindableProperty NameProperty =
            BindableProperty.Create("Name", typeof(string), typeof(UserSelectableCell), null);
        public static readonly BindableProperty RoleProperty =
            BindableProperty.Create("Role", typeof(string), typeof(UserSelectableCell), null);
        public static readonly BindableProperty UsernameProperty =
            BindableProperty.Create("Username", typeof(string), typeof(UserSelectableCell), null);

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public string Role
        {
            get { return (string)GetValue(RoleProperty); }
            set { SetValue(RoleProperty, value); }
        }

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        Label lbName, lbWeight, lbHeight;

        public UserSelectableCell()
        {
            lbName = new Label { HorizontalOptions = LayoutOptions.StartAndExpand };
            lbWeight = new Label { HorizontalOptions = LayoutOptions.EndAndExpand };
            lbHeight = new Label { HorizontalOptions = LayoutOptions.EndAndExpand };

            Grid infoLayout = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(3,GridUnitType.Star)},
                    new ColumnDefinition {Width = new GridLength(1,GridUnitType.Star)},
                    new ColumnDefinition {Width = new GridLength(1,GridUnitType.Star)}
                },
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            infoLayout.Children.Add(lbName, 0, 0);
            infoLayout.Children.Add(lbWeight, 1, 0);
            infoLayout.Children.Add(lbHeight, 2, 0);

            var cellWrapper = new Grid
            {
                Padding = 10,
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(1,GridUnitType.Auto)},
                    new ColumnDefinition {Width = new GridLength(1,GridUnitType.Star)},
                }
            };

            var sw = new Switch();
            sw.SetBinding(Switch.IsToggledProperty, "IsSelected");
            cellWrapper.Children.Add(sw, 0, 0);
            cellWrapper.Children.Add(infoLayout, 1, 0);

            View = cellWrapper;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if(BindingContext != null)
            {
                lbName.Text = Name;
                lbWeight.Text = Role;
                lbHeight.Text = Username;
            }
        }
    }
}
