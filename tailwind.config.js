/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: 'class',
  content: [
        './Layout/**/*.razor',
        './Pages/**/*.razor',
        './wwwroot/**/*.css',
        './node_modules/flowbite/**/*.js'
  ],
  theme: {},
  plugins: [
      require('flowbite/plugin')
  ],
}

