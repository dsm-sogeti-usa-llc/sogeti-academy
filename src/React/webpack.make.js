var webpack = require('webpack');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var StringReplacePlugin = require('string-replace-webpack-plugin');
var path = require('path');

function getHtmlPlugin() {
    return new HtmlWebpackPlugin({
        template: 'src/index.html',
        inject: 'body'
    });
}

function getPlugins(env) {
    var plugins = [];
    plugins.push(new webpack.optimize.DedupePlugin());
    plugins.push(new StringReplacePlugin());
    plugins.push(getHtmlPlugin());
    return plugins;
}

function getOutputPath(env) {
    return env === 'prod'
        ? path.join(__dirname)
        : path.join(__dirname, 'dist');
}

module.exports = function (env) {
    return {
        devtool: 'sourcemap',
        entry : {
            index: './src/index.jsx'
        },
        output: {
            path: getOutputPath(env),
            filename: 'js/[name].js',
            sourceMapFilename: '[file].map',
            chunkFilename: 'js/[id].js'
        },
        resolve: {
            extensions: ['', '.js', '.jsx', '.css', '.scss', '.html']
        },
        module: {
            loaders: [
                {
                    test: /\.(js|jsx)$/,
                    loader: 'babel'
                },
                {
                    test: /\.(css|scss)$/,
                    loader: 'style!css!sass'
                },
                {
                    test: /config\.js$/,
                    loader: StringReplacePlugin.replace({
                        replacements: [
                            {
                                pattern: /\'\'/,
                                replacement: function(match, p1, offset, string) {
                                    return "'" + process.env['Topics:ApiUrl'] + "'";
                                }
                            }
                        ]
                    })
                },
                {
                    test: require.resolve('jquery'),
                    loader: 'expose?$!expose?jQuery'  
                },
                {
                    test: /telemetry\.js$/,
                    loader: StringReplacePlugin.replace({
                        replacements: [
                            {
                                pattern: /\{key\}/,
                                replacement: function(match, p1, offset, string) {
                                    return process.env['ApplicationInsights:InstrumentationKey'];
                                }
                            }
                        ]
                    })
                }
            ]
        },
        plugins: getPlugins(env),
        devServer: {
            port: 8081
        }
    };
};