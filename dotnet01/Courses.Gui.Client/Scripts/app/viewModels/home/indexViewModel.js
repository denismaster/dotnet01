define(['kendo'],
    function(kendo) {

        var indexViewModel = new kendo.observable(
            {
                productsSource: new kendo.data.DataSource({
                    transport: {
                        read: {
                            url: "/api/Product",
                            datatype: "xml",
                            schema: {
                                type: "xml",
                                data: "/api/Product",
                                model: {
                                    fields: {
                                        Name: "Name/text()",
                                        Description: "Description/text()"
                                    }
                                }
                            }
                        }
                    }
                }),
                content: "",
            });

        return indexViewModel;
    });