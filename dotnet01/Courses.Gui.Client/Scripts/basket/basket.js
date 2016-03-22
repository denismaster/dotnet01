
function BasketButtonEventListener()
{
    var id = $(target).attr("data-id");
    alert(id);
}

function AddToBasket(item)
{
    var basket = RestoreBasket();
    basket.push(item);
    SaveBasketState(basket);
}

function GetBasket()
{
    alert(GetBasketItems());
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

function GetBasketItems()
{
    var basket = RestoreBasket();
    return basket;
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