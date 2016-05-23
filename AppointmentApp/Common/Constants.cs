using System;

namespace AppointmentApp.Constants
{
	public  class Constants
	{
		public static string MESSEGE_CENTER_OPEN_DOCTOR_PAGE = "MESSEGE_CENTER_OPEN_DOCTOR_PAGE";
		public static string JSON_APPOINTMENTS= @"[
  {
    'id':'1',
    'doctor_id':'1',
    'date':'5/24/2016 3:45:59 PM',
    'notes':['notes1', 'notes2','notes3']
  },
{
    'id':'2',
    'doctor_id':'1',
    'date':'5/24/2016 3:45:59 PM',
    'notes':['notes1', 'notes2','notes3']
  },
{
    'id':'3',
    'doctor_id':'2',
    'date':'5/24/2016 3:45:59 PM',
    'notes':['notes1', 'notes2','notes3']
  },
{
    'id':'4',
    'doctor_id':'2',
    'date':'5/24/2016 3:45:59 PM',
    'notes':['notes1', 'notes2','notes3']
  }
]";

		public static string JSON_DOCTORS   = @"[
  {
    'id':'1',
    'name':'Saqib Usman',
    'opening_hours':['Mon: 9am - 1:00pm', 'Tue: 9am - 5:00pm', 'Wed:9:00am - 10:00pm'],
    'street': 'Weser Strasse 69',
	'postal_code': '12555',
	'city':'Berlin'
  },
{
    'id':'2',
    'name':'Christian Blessing',
    'opening_hours':['Mon: 9am - 1:00pm', 'Tue: 9am - 5:00pm', 'Wed:9:00am - 10:00pm'],
    'street': 'Weser Strasse 69',
	'postal_code': '12045',
	'city':'Berlin'
  }

]";


	}
}

