
$(document).ready(function () {
    $(".product-details").hide();
    $(".category").hide();
    //$("#treelist").hide();
    var dataSource;
    var selectedItem;
    var searchValue = "";
    var mainFilter = { logic: "and", filters: [] };

    function clearFilter() {
        dataSource.filter({});
        mainFilter.filters = [];
    }
    function applyFilter() {
        dataSource.filter(mainFilter);
    };
    function addFilter(newFilter)
    {
        mainFilter.filters.push(newFilter);
        applyFilter();
    }

    $(function () {
    categoryDataSource = new kendo.data.TreeListDataSource({
        transport: {
            read: {
                url: "/api/Category",
                dataType: "json"
            }
        },
        schema: {
            model: {
                id: "Id",
                fields:{
                    Id: {type: "number", nullable:false},
                    parentId: { field: "ParentCategoryId", nullable: true },
                    Name: {field: "Name"}
                },
                expanded: true
            }
        }
    });
    $("#treelist").kendoTreeList({
        dataSource: categoryDataSource,
        columns: [
            { field: "Name", title: "Категория" }
        ],
        selectable: 'row',
        change: function(e) {
            var selectedRows = this.select();
            var selectedDataItems = [];
            for (var i = 0; i < selectedRows.length; i++) {
                var dataItem = this.dataItem(selectedRows[i]);
                selectedDataItems.push(dataItem);
            }
            console.log(selectedDataItems[0]);
            clearFilter();
            addFilter({
                field: 'CategoriesNamesString',
                operator: 'contains',
                value: selectedDataItems[0]["Name"]
            })
        }

    });
    });

    $(function () {
        $("#toolbar").kendoToolBar({
            items: [
                {
                    type: "buttonGroup",
                    buttons: [
                        { type: "button", id: "button1", text: "Все", group: "activity", click: function (e) { clearFilter();} },
                        {
                            type: "button", id: "button2", text: "Активные", group: "activity", click: function (e) {
                                addFilter({ field: 'Active', operator: 'eq', value: true });
                            }
                        },
                        {
                            type: "button", id: "button3", text: "Неактивные", group: "activity", click: function (e) {
                                addFilter({ field: 'Active', operator: 'eq', value: false });
                            }
                        }
                    ]
                },
                { type: "separator" },
                { template: "<label>Тип мероприятия:</label>" },
                {
                    template: "<input id='dropdown' style='width: 150px;' />",
                    overflow: "never"
                },
                { type: "separator" }
            ]
        });

        $("#dropdown").kendoDropDownList({
            dataTextField: "text",
            dataValueField: 0,
            dataSource: [
                { text: "Любые", value: 0},
                { text: "Курсы", value: 1 },
                { text: "Лекции", value: 2 },
                { text: "Мастер-класс", value: 3 },
                { text: "Подготовка к экзаменам", value: 4 },
                { text: "Практические занятия", value: 5 }
            ],
            select: function (e) {
                var selectedDataItem = this.dataItem(e.item);
                console.log(selectedDataItem.value);
                if (selectedDataItem.value === 0) {
                    clearFilter();
                    return;
                }
                addFilter({ field: 'Type', operator: 'eq', value: selectedDataItem.value })
            }
        });






    });


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
                activeValue: function(){
                    if (selectedItem["Active"] === true) {
                        $("#img-box span").removeClass().addClass("label label-success")
                        return "Курсы активны";
                    }
                    else {
                        $("#img-box span").removeClass().addClass("label label-danger")
                        return "Курсы не активны";
                    }
                },
                typeValue: selectedItem["Type"],
                locationValue: selectedItem["Location"],
                seatsCountValue: selectedItem["SeatsCount"],
                photoAsBase64: function () {
                    return "data:image/jpeg;base64," + selectedItem["Image"];
                },
                teacherValue: selectedItem["Teacher"],
                managerValue: selectedItem["ManagerName"]
            });
            kendo.bind($(".product-details"), viewModel);
            $(".index").hide();
            $(".product-details").show();      
        }
    });



    $(document).on('click', '#backButton', function () {
        $(".index").show();
        $(".product-details").hide();
        $("#listView").data("kendoListView").clearSelection();
    });
    $(document).on('input', '#search', '[data-action="text"]', function () {
        searchValue = $("#search").val();
        if (searchValue.length > 0) {
            addFilter({ field: 'Name', operator: 'contains', value: searchValue })
        } else {
            clearFilter();
        }
    });

});
