define(['kendo'],
    function(kendo) {
        var registrationViewModel = new kendo.observable(
            {
                content: "",
            });

        return registrationViewModel;
    });