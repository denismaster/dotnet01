function GetItemRow(item ,index)
{
	//Создаем строку
	var row = document.createElement("tr");
	//Создаем ячейки
	var indexCell = document.createElement("td");
	var keyCell = document.createElement("td");
	var nameCell = document.createElement("td");
	var priceCell = document.createElement("td");
	//наполнение данных
	indexCell.innerText = "" + index;
	keyCell.innerText = "" + item.id;
	//Тут получение данных с помощью Kendo
	nameCell.innerText = "text" + index;
	priceCell.innerText = 100 + "$";
	
	row.appendChild(indexCell);
	row.appendChild(keyCell);
	row.appendChild(nameCell);
	row.appendChild(priceCell);

	return row;
}

function GetItemRows(table,basket)
{
    for(i=0;i<basket.length;i++)
    {
        table.appendChild(GetItemRow(basket[i], i));
    }
}

function DrawBasket()
{
    var table = document.getElementById("basket-table");
    var basket = RestoreBasket();
    GetItemRows(table, basket);
}
