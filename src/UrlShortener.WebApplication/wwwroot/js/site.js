// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function openShortUrlInNewTab(givenAlias) {
    // Replace 'alias' with the actual alias of the short URL you want to redirect to
    const alias = givenAlias;

    // Replace the URL below with the base URL of your API, followed by the 'Redirection' endpoint and the 'alias' parameter
    const redirectionUrl = 'https://http://localhost:5167/ShortUrlApi/Redirection/' + alias;

    // Open the redirection URL in a new tab
    window.open(redirectionUrl, '_blank');
}
