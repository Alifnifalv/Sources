using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eduegate.Integrations.Engine.DbContexts.Models
{
	public class Student
	{
		[Key]
		public string sims_student_enroll_number { get; set; }
		//public string sims_student_cur_code { get; set; }
		//public string sims_student_passport_first_name_en { get; set; }
		//public string sims_student_passport_middle_name_en { get; set; }
		//public string sims_student_passport_last_name_en { get; set; }
		//public string sims_student_family_name_en { get; set; }
		//public string sims_student_nickname { get; set; }
		//public string sims_student_passport_first_name_ot { get; set; }
		//public string sims_student_passport_middle_name_ot { get; set; }
		//public string sims_student_passport_last_name_ot { get; set; }
		//public string sims_student_family_name_ot { get; set; }
		//public string sims_student_gender { get; set; }
		//public string sims_student_religion_code { get; set; }
		//public DateTime? sims_student_dob { get; set; }
		//public string sims_student_birth_country_code { get; set; }
		//public string sims_student_nationality_code { get; set; }
		//public string sims_student_ethnicity_code { get; set; }
		//public string sims_student_visa_number { get; set; }
		//public DateTime? sims_student_visa_issue_date { get; set; }
		//public DateTime? sims_student_visa_expiry_date { get; set; }
		//public string sims_student_visa_issuing_place { get; set; }
		//public string sims_student_visa_issuing_authority { get; set; }
		//public string sims_student_visa_type { get; set; }
		//public string sims_student_national_id { get; set; }
		//public DateTime? sims_student_national_id_issue_date { get; set; }
		//public DateTime? sims_student_national_id_expiry_date { get; set; }
		//public string sims_student_main_language_code { get; set; }
		//public string sims_student_main_language_r { get; set; }
		//public string sims_student_main_language_w { get; set; }
		//public string sims_student_main_language_s { get; set; }
		//public string sims_student_main_language_m { get; set; }
		//public string sims_student_other_language { get; set; }
		//public string sims_student_primary_contact_code { get; set; }
		//public string sims_student_primary_contact_pref { get; set; }
		//public string sims_student_fee_payment_contact_pref { get; set; }
		//public string sims_student_transport_status { get; set; }
		//public string sims_student_parent_status_code { get; set; }
		//public string sims_student_legal_custody { get; set; }
		//public string sims_student_emergency_contact_name1 { get; set; }
		//public string sims_student_emergency_contact_name2 { get; set; }
		//public string sims_student_emergency_contact_number1 { get; set; }
		//public string sims_student_emergency_contact_number2 { get; set; }
		//public string sims_student_language_support_status { get; set; }
		//public string sims_student_language_support_desc { get; set; }
		//public string sims_student_behaviour_status { get; set; }
		//public string sims_student_behaviour_desc { get; set; }
		//public string sims_student_gifted_status { get; set; }
		//public string sims_student_gifted_desc { get; set; }
		//public string sims_student_music_status { get; set; }
		//public string sims_student_music_desc { get; set; }
		//public string sims_student_sports_status { get; set; }
		//public string sims_student_sports_desc { get; set; }
		//public DateTime? sims_student_date { get; set; }
		//public DateTime? sims_student_commence_date { get; set; }
		//public string sims_student_remark { get; set; }
		//public string sims_student_login_id { get; set; }
		//public string sims_student_current_school_code { get; set; }
		//public string sims_student_employee_comp_code { get; set; }
		//public string sims_student_employee_code { get; set; }
		//public DateTime? sims_student_last_login { get; set; }
		//public string sims_student_secret_question_code { get; set; }
		//public string sims_student_secret_answer { get; set; }
		//public string sims_student_academic_status { get; set; }
		//public string sims_student_financial_status { get; set; }
		//public string sims_student_house { get; set; }
		public string sims_student_img { get; set; }
		//public string sims_student_class_rank { get; set; }
		//public string sims_student_honour_roll { get; set; }
		//public string sims_student_ea_number { get; set; }
		//public DateTime? sims_student_ea_registration_date { get; set; }
		//public string sims_student_ea_transfer { get; set; }
		//public string sims_student_ea_status { get; set; }
		//public string sims_student_passport_number { get; set; }
		//public DateTime? sims_student_passport_issue_date { get; set; }
		//public DateTime? sims_student_passport_expiry_date { get; set; }
		//public string sims_student_passport_issuing_authority { get; set; }
		//public string sims_student_passport_issue_place { get; set; }
		//public string sims_student_admission_grade_code { get; set; }
		//public string sims_student_admission_academic_year { get; set; }
		//public string sims_student_prev_school { get; set; }
		//public string sims_student_admission_number { get; set; }
		//public string sims_student_attribute1 { get; set; }
		//public string sims_student_attribute2 { get; set; }
		//public string sims_student_attribute3 { get; set; }
		//public string sims_student_attribute4 { get; set; }
		//public string sims_student_attribute5 { get; set; }
		//public string sims_admission_fee_month_code { get; set; }
		//public string sims_fee_termOption { get; set; }
		//public string sims_student_attribute8 { get; set; }
		//public string sims_student_attribute12 { get; set; }
		//public string sims_student_learning_therapy_status { get; set; }
		//public string sims_student_learning_therapy_desc { get; set; }
		//public string sims_student_special_education_status { get; set; }
		//public string sims_student_special_education_desc { get; set; }
		//public string sims_student_falled_grade_status { get; set; }
		//public string sims_student_falled_grade_desc { get; set; }
		//public string sims_student_communication_status { get; set; }
		//public string sims_student_communication_desc { get; set; }
		//public string sims_student_specialAct_status { get; set; }
		//public string sims_student_specialAct_desc { get; set; }
		//public string sims_student_pan_no { get; set; }
		//public string sims_student_voter_id { get; set; }
		//public string sims_student_passport_full_name_en { get; set; }
		//public string sims_student_passport_full_name_ar { get; set; }
		//public string sims_student_attribute7 { get; set; }
		//public string sims_student_attribute10 { get; set; }
		//public string sims_student_attribute6 { get; set; }

		////sims_student_section
		//public string sims_cur_code { get; set; }
		//public string sims_academic_year { get; set; }
		//public string sims_grade_code { get; set; }
		//public string sims_section_code { get; set; }
		//public string sims_enroll_number { get; set; }
		//public string sims_allocation_status { get; set; }
		//public string sims_roll_number { get; set; }
		//public string sims_fee_category_code { get; set; }
		//public string sims_grade_section_entry_status { get; set; }
		//public DateTime? sims_grade_section_entry_date { get; set; }
		//public string Class { get; set; }
		//public string Section { get; set; }

		//public string Religion { get; set; }
		//public string Country { get; set; }

	}
	//public class Student
	//{

	//    [Key]
	//    public string AdmissionNumber { get; set; }
	//    public string FirstName { get; set; }
	//    public string RollNumber { get; set; }
	//    public string Class { get; set; }
	//    public string Section { get; set; }
	//    public string Religion { get; set; }
	//    public string Country { get; set; }
	//    public string MiddleName { get; set; }
	//    public string LastName { get; set; }
	//    public string GenderID { get; set; }
	//    public DateTime?? DateOfBirth { get; set; }


	//    //StudentID = toDto.StudentPassportDetails.StudentID,

	//    //public int NationalityID { get; set; }
	//    public string PassportNo { get; set; }

	//    public string CountryofIssueID { get; set; }
	//    public DateTime?? PassportNoExpiry { get; set; }
	//    public string CountryofBirthID { get; set; }
	//    public string VisaNo { get; set; }
	//    public string VisaExpiry { get; set; }
	//    public string NationalIDNo { get; set; }
	//    public DateTime?? NationalIDNoExpiry { get; set; }
	//    public DateTime?? StudentNationalIDNoIssueDate { get; set; }
	//    public DateTime?? StudentPassportNoIssueDate { get; set; }


	//}
}
