
function BasketButtonEventListener()
{
    var button = $(event.target);
    button.hide();
    var id = button.attr("data-id");
    AddToBasket(id);
}

function AddToBasket(item)
{
    var basket = RestoreBasket();
    var basketItem = { id:item,count:1}
    basket.push(basketItem);
    SaveBasketState(basket);
}

function RemoveFromBasket(item)
{
    var index = 0;
    for (i = 0; i < basket.length; i++)
    {
        if(basket[i]==item)
        {
            index = i;
        }
    }
    basket.splice(index, 1);
}

function SaveBasketState(basket)
{
    localStorage.setItem("basket", JSON.stringify(basket));
}

function RestoreBasket()
{
    var basket = JSON.parse(localStorage.getItem("basket"));
    if (basket == null || basket == undefined)
        return [];
    else
        return basket;
}