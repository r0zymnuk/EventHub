module.exports = {
    darkMode: 'class',
    theme: {
        extend: {
            colors: {
                'pers-green': '#2F9C95',
                'mint': '#40C9A2',
                'light-green': '#A3F7B5',
                'nyanza': '#E5F9E0'
            }
        },
    },
    variants: {},
    plugins: [
        require('@tailwindcss/forms'),
        require('flowbite/plugin')
    ]
};
