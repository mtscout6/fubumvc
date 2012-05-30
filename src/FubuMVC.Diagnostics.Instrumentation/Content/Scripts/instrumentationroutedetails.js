$(function() {
  var arrow = $('#chain-arrow').html();
  $('.chain-visualizer > li:not(:last)').after('<li class="arrow">' + arrow + '</li>');

  $('div.alert-message').click(function(event) {
    var element = $(event.currentTarget),
      sibling = element.next('div');
    if(sibling.is(':visible')) {
      sibling.slideToggle(250);
    }
    else {
      $.ajax({
        url: 'details',
        type: 'GET',
        data: { Id: '', reportId: '' },
        dataType: 'json',
        contentType: 'application/json',
        success: function(data) {
          sibling.slideToggle(250);
        }
      });
    }
  });
});