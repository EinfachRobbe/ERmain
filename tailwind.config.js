/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: 'class',
  content: [
        './Layout/**/*.razor',
        './Pages/**/*.razor',
        './wwwroot/**/*.css',
        './node_modules/flowbite/**/*.js',
        './Mailing/Templates/*.html'
  ],
  theme: {},
  plugins: [
      require('flowbite/plugin')
  ],
}

