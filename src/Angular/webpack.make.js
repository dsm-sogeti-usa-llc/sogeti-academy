var webpack = require('webpack');
var path = require('path');
var HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = function(env) {
    return {
        devtool: 'sourcemap',
        entry: {
            vendor: './src/vendor.ts',
            index: './src/index.ts'
        },
        output: {
            path: path.join(__dirname, 'dist'),
            filename: 'js/[name].js',
            sourceMapFilename: '[file].map'
        },
        resolve: {
            extensions: ['', '.ts', '.js', '.scss', '.css', '.html']
        },
        module: {
            loaders: [
                {
                    test: /\.ts$/,
                    loader: 'ts'
                },
                {
                    test: /\.(scss|css)$/,
                    loader: 'style-loader!css-loader!sass-loader'
                },
                {
                    test: /\.html$/,
                    loader: 'html'
                }
            ]
        },
        plugins: [
            new HtmlWebpackPlugin({
                template: './src/index.html',
                inject: 'body'
            })
        ]
    }
}