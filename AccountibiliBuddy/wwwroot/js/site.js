﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//code to restrict choosing a past date to play around with later
//var today = new Date().toISOString().split('T')[0];
//document.getElementsByName("createDate")[0].setAttribute('min', today);








$('.carousel').carousel()



document.querySelectorAll("#complete").forEach(item => {
    item.addEventListener('click', event => {
        console.log(event.target.id)
        fetch(`/Goals/Complete/${event.target.id}`)
            .then(() => { window.location.reload(true) })
    })
})