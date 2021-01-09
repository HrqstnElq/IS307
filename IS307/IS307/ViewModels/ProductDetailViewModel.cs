﻿using IS307.Models;
using IS307.Services;
using IS307.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {
        public ICommand GoBackCommand { get; set; }
        public ICommand Increment { get; set; }
        public ICommand Decrement { get; set; }
        public ICommand AddToCart { get; set; }
        public ICommand Favorite { get; set; }


        private ProductModel product;
        public ProductModel Product
        {
            get => product;
            set => SetProperty(ref product, value);

        }

        private int quantity = 1;
        public int Quantity
        {
            get => quantity;
            set => SetProperty(ref quantity, value);
        }
        public bool isFavorite { get; set; }

        private readonly ProductService productService = new ProductService();

        public ProductDetailViewModel(INavigation navigation, ProductModel productModel)
        {
            var token = Application.Current.Properties["token"].ToString();

            LoadPageCommand = new Command(async () =>
            {
                Product = productModel;
                isFavorite = await productService.IsFavoriteProduct(Product._id, token);
                OnPropertyChanged(nameof(Favorite));
                IsBusy = false;
            });


            GoBackCommand = new Command(() =>
            {
                navigation.PopAsync();
            });
            
            Increment = new Command(() =>
            {
                Quantity = Quantity + 1;
            });


            Decrement = new Command(() =>
            {
                Quantity = Quantity < 2 ? Quantity : Quantity - 1;
            });

            AddToCart = new Command(() =>
            {
                App.Database.SaveCart(new CartItemModel()
                {
                    productId = Product._id,
                    name = Product.name,
                    pictureUrl = Product.pictureUrl,
                    price = Product.price,
                    quantity = Quantity
                });
                OnPropertyChanged(nameof(AddToCart));
            });

            Favorite = new Command(() =>
            {
                OnPropertyChanged("Loading");
                if (isFavorite)
                {
                   productService.UnFavoriteProduct(Product._id, token);
                }
                else
                {
                    productService.FavoriteProduct(Product._id, token);
                }
                
                isFavorite = !isFavorite;
                OnPropertyChanged(nameof(Favorite));
                OnPropertyChanged("Complete");
            });
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
