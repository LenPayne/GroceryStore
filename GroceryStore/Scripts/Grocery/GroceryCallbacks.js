function SuccessCallback(result) {
    $('#ajax-result').text(result.text);
}

function FailureCallback(result) {    
    alert(result.responseJSON.text);
}