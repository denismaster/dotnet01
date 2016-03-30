
$(document).ready(function () {
    $(".product-details").hide();
    $(".category").hide();
    var dataSource;
    var selectedItem;
    var searchValue="";
    $(function () {
        dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: '/api/Product',
                    dataType: 'json'
                }
            },
            pageSize: 12
        });

        $("#pager").kendoPager({
            dataSource: dataSource
        });

        $("#listView").kendoListView({
            dataSource: dataSource,
            serverFiltering: false,
            selectable: "single",
            change: onChange,
            template: kendo.template($("#template").html())
        });
        function onChange() {
            
            var data = dataSource.view(),
                selected = $.map(this.select(), function (item) {
                    return data[$(item).index()];
                });
            selectedItem = selected[0];
            var viewModel = kendo.observable({
                nameValue: selectedItem["Name"],
                descriptionValue: selectedItem["Description"],
                activeValue: selectedItem["Active"],
                typeValue: selectedItem["Type"],
                locationValue: selectedItem["Location"],
                seatsCountValue: selectedItem["SeatsCount"]
            });
            kendo.bind($(".product-details"), viewModel);
            $(".index").hide();
            $(".product-details").show();      
        }
    });
    $("#textButton").kendoButton({
        click: onClick
    });
    function onClick() {
        $(".index").show();
        $(".product-details").hide();
        $("#listView").data("kendoListView").clearSelection();
    }


    function applyFilter(){
        dataSource.filter([
            {
                field: 'Name',
                operator: 'contains',
                value: searchValue
            }
        ]);
    }

    function clearFilter() {
        dataSource.filter({});
    }

    $(document).on('input', '#search', '[data-action="text"]', function () {
        searchValue = $("#search").val();
        if (searchValue.length > 0) {
            applyFilter();
        } else {
            clearFilter();
        }
    });

});
