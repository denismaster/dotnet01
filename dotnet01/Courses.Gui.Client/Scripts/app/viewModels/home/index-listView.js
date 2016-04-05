
$(document).ready(function () {
    $(".product-details").hide();
    $(".category").hide();
    var dataSource;
    var selectedItem;
    var searchValue="";

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
                { field: "Name" }
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
                dataSource.filter([
                    {
                        field: 'CategoriesNamesString',
                        operator: 'contains',
                        value: selectedDataItems[0]["Name"]
                    }
                ]);
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
                teacherValue: selectedItem["Teacher"]
            });
            kendo.bind($(".product-details"), viewModel);
            $(".index").hide();
            $(".product-details").show();      
        }
    });


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

    $(document).on('click', '#backButton', function () {
        $(".index").show();
        $(".product-details").hide();
        $("#listView").data("kendoListView").clearSelection();
    });
    $(document).on('input', '#search', '[data-action="text"]', function () {
        searchValue = $("#search").val();
        if (searchValue.length > 0) {
            applyFilter();
        } else {
            clearFilter();
        }
    });

});
