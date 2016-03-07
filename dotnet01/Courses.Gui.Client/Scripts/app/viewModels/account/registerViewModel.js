define(['kendo'],
    function(kendo) {
        var registerViewModel = new kendo.observable(
            {
                content: "",
            });

        return registerViewModel;
    });
