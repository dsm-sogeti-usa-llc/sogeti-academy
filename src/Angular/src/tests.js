require('./vendor');
require('./index');
require('angular-mocks');

var testsContext = require.context('.', true, /Spec$/);
testsContext.keys().forEach(testsContext);