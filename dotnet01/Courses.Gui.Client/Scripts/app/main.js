require.config(
{
paths:{
'jquery':'/scripts/jquery-2.0.3.min',
'kendo':'/scripts/kendo.web.min',
'text':'/scripts/text',
'router':'/scripts/app/router',
'account-registerViewModel': '/scripts/app/viewModels/account/registerViewModel'
},
shim : {
    'kendo' : ['jquery']
  },
  priority: ['text', 'router', 'app'],
  jquery: '2.0.3',
  waitSeconds: 1
});
require([
  'app'
], function (app) {
  app.initialize();
});