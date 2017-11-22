var events = {
	// sample events
	'12-25-2000' : [{content: 'Christmas Day', repeat: 'YEARLY', allDay: true, endDate: '12-25-2100'}],
	'01-01-2000' : [{content: 'New Year\'s', repeat: 'YEARLY', allDay: true, endDate: '12-31-2100'}],
},
t = new Date(),
//Creation of today event
today = ((t.getMonth() + 1) < 10 ? '0' + (t.getMonth() + 1) : (t.getMonth() + 1)) + '-' + (t.getDate() < 10 ? '0' + t.getDate() : t.getDate()) + '-' +t.getFullYear();
events[today] = [{content: 'TODAY', allDay: true}];
console.log('data.js init');
