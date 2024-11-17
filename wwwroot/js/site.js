// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function addDaysToDate(date, days) {
    // Ensure the input date is a Date object
    const result = new Date(date);
    // Add the specified number of days
    result.setDate(result.getDate() + days);
    return result;
}