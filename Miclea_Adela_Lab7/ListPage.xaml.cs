using Miclea_Adela_Lab7.Models;
namespace Miclea_Adela_Lab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        Product product;
        var shopList = (ShopList)BindingContext;
        if (listView.SelectedItem != null)
        {
            product = listView.SelectedItem as Product;
            var listProductAll = await App.Database.GetListProducts();
            var listProduct = listProductAll.FindAll(x => x.ProductID == product.ID & x.ShopListID == shopList.ID);
            await App.Database.DeleteListProductAsync(listProduct.FirstOrDefault());
            await Navigation.PopAsync();

        }
    }
        async void OnChooseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
            {
                BindingContext = new Product()
            });
        
        }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}