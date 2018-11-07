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
            options: {           // ���û��options���ѡ��ᱨ�� No PostCSS Config found
                plugins: (loader) => [
                    require('autoprefixer')(), //CSS���������
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