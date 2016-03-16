var webpack = require('webpack');
var path = require('path');
var HtmlWebpackPlugin = require('html-webpack-plugin');

function getEntry(env) {
    if (env === 'test') {
        return undefined;
    }
    return {
        vendor: './src/vendor.ts',
        index: './src/index.ts'
    };
}

module.exports = function(env) {
    return {
        devtool: 'sourcemap',
        entry: getEntry(env),
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