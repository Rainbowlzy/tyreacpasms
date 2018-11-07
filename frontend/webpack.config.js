modules:{
    rules: [
        {
            test: /\.(png|jpg|gif|ico)$/,
            exclude: /(node_modules|bower_components)/,
            use: {
                loader: 'url-loader',
                options: {
                    presets: ['@babel/preset-env']
                }
            }
        },
        {
            loader:"postcss-loader",
            options: {           // 如果没有options这个选项将会报错 No PostCSS Config found
                plugins: (loader) => [
                    require('autoprefixer')(), //CSS浏览器兼容
                ]
            }
        },
        {
            test: /\.(woff|woff2|eot|ttf|svg)$/,
            use: [
                'file-loader'
            ]
        }
    ]
}