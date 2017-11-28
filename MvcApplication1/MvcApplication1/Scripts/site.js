$(function(){
	$('.yt_vid').on('click', function(){
		var value = $(this).attr('data-value');
		var title = $(this).attr('alt');
		$('#mod_title').html(title);
		$('#yt_frame').attr('src', 'http://www.youtube.com/embed/' + value);
		$('#mod_youtube').modal('show');
	});
	
	$('.openchat').on('click', function(){
		
		$('.chatbox').toggleClass('closed');
		$('fa').toggleClass('fa-angle-down');
		/*$(this).removeClass('openchat').addClass('closechat');*/
		
	});
	
	$('#btnChat').on('click', function(){
		$('#chatform').fadeOut(function(){ $('#chatlog').fadeIn() });
		return false;
	});
	
		
	
	function initialize() {
	  var mapOptions = {
		zoom: 12,
		center: new google.maps.LatLng(29.73693, -95.52388),
		disableDefaultUI: true
	  }
	  var map = new google.maps.Map(document.getElementById('map'),
									mapOptions);
	}

	google.maps.event.addDomListener(window, 'load', initialize);
});

	
