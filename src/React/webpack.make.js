var webpack = require('webpack');
var HtmlWebpackPlugin = require('html-webpack-plugin');
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
    plugins.push(getHtmlPlugin());
    return plugins;
}

module.exports = function (env) {
    return {
        devtool: 'sourcemap',
        entry : {
            index: './src/index.jsx'
        },
        output: {
            path: path.join(__dirname, 'dist'),
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
                }
            ]
        },
        plugins: getPlugins(env) 
    };
};