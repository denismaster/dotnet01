define(['kendo'],
    function (kendo) {
        var router = new kendo.Router(),
            layout = new kendo.Layout("<div id='content'></div>");

        layout.render($("#app"));

        router.route("/", function () {
            require(['text!/home/index'], function (view) {
                loadView(null, view);
            });
        });
        router.route("/home/basket", function () {
            require(['text!/home/basket'], function (view) {
                loadView(null, view);
            });
        });

        router.route("/home/about", function () {
            require(['text!/home/about'], function (view) {
                loadView(null, view);
            });
        });

         router.route("/home/register", function () {
            require(['text!/home/register'], function (view) {
                loadView(null, view);
            });
         });
         router.route("/home/test", function () {
             require(['text!/home/test'], function (view) {
                 loadView(null, view);
             });
         });

        router.route("/home/contact", function () {
            require(['text!/home/contact'], function (view) {
                loadView(null, view);
            });
        });
        router.route("/account/register", function () {
            require([ 'text!/account/register'], function ( view) {
                loadView(null, view) 
            });
        });


        var loadView = function (viewModel, view, delegate) {
            var kendoView = new kendo.View(view, { model: viewModel });
            kendo.fx($("#content")).slideInRight().reverse().then(function () {
                layout.showIn("#content", kendoView);

                if (delegate != undefined)
                    delegate();

                kendo.fx($("#content")).slideInRight().play();
            });
        };

        return router;
    });
