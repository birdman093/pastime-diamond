const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const { CleanWebpackPlugin } = require("clean-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = {
    entry: {
        login: "./src/ts/login.ts",
        chat: "./src/ts/chat.ts",
    },
    output: {
        path: path.resolve(__dirname, "wwwroot"),
        filename: "[name].js",
        publicPath: "/",
    },
    resolve: {
        extensions: [".js", ".ts"],
    },
    module: {
        rules: [
            {
                test: /\.ts$/,
                use: "ts-loader",
            },
            {
                test: /\.css$/,
                use: [MiniCssExtractPlugin.loader, "css-loader"],
            },
        ],
    },
    plugins: [
        new CleanWebpackPlugin(),
        /* Needed for non-razor html pages
        new HtmlWebpackPlugin({
            template: "./src/login.html",
            chunks: ['login'],
            filename: "login.html"
        }),
        new HtmlWebpackPlugin({
            template: "./src/chat.html",
            chunks: ['chat'],
            filename: "chat.html"
        }),
        */
        new MiniCssExtractPlugin({
            filename: "css/[name].[chunkhash].css",
        }),
    ],
};