$(function() {
  var arrow = $('#chain-arrow').html();
  $('.chain-visualizer > li:not(:last)').after('<li class="arrow">' + arrow + '</li>');

  $('div.alert-message').click(function(event) {
    var element = $(event.currentTarget),
        sibling = element.next('div'),
        chainId = $('#chainId').val(),
        reportId = element.find('input').val();

    if(sibling.is(':visible') || $.data(sibling[0], 'loaded') === 'true') {
      sibling.slideToggle(250);
    }
    else {
      $.ajax({
        url: 'details',
        type: 'GET',
        data: { Id: chainId, ReportId: reportId },
        success: function(data) {
          sibling.slideToggle(250);
          if (data.Behaviors) {
            $('#detailsTemplate').tmpl(data.Behaviors).appendTo(sibling);
          } else {
            $('#noDetailsTemplate').tmpl(data).appendTo(sibling);
          }
          $.data(sibling[0], 'loaded', 'true');
        }
      });
    }
  });
});