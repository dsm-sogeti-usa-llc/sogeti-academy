// Karma configuration
// Generated on Mon Mar 14 2016 23:33:22 GMT-0500 (Central Daylight Time)

module.exports = function(config) {
    config.set({
        basePath: '',
        frameworks: ['jasmine'],
        files: [
            './src/vendor.ts',
            './src/index.ts',
            './src/tests.js'
        ],
        exclude: [
        ],
        preprocessors: {
            './src/vendor.ts': ['webpack', 'sourcemap'],
            './src/index.ts': ['webpack', 'sourcemap'],
            './src/tests.js': ['webpack', 'sourcemap']
        },
        reporters: ['progress'],
        port: 9876,
        colors: true,
        logLevel: config.LOG_INFO,
        autoWatch: true,
        browsers: ['PhantomJS'],
        singleRun: false,
        concurrency: Infinity,
        webpack: require('./webpack.test')
    })
}
