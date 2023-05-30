package com.jorgebg.classprobuilder.student

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.*
import androidx.fragment.app.DialogFragment
import com.jorgebg.classprobuilder.R
import com.jorgebg.classprobuilder.databases.Student_Subject_Provisional
import com.jorgebg.classprobuilder.databases.SubjectProvisional
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch

class dialogInsertSubjectsStudent(val view: student_signature, val presenter : presenterStudentSubject,val idStudent : Int) : DialogFragment(){

    lateinit var newList : ArrayList<String>

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        var rootView : View = inflater.inflate(R.layout.insert_subject,container,false)

        val listSubjects : List<SubjectProvisional> = view.listSubjects

        newList= arrayListOf()

        for (element in listSubjects){
            newList.add(element.nombre)
        }

        val spinerSubjects = rootView.findViewById<Spinner>(R.id.spinerSubjects)
        val editTrim = rootView.findViewById<EditText>(R.id.editTrimestral)

        val adapter = ArrayAdapter(view.applicationContext, android.R.layout.simple_dropdown_item_1line, newList)

        spinerSubjects.apply {
            setAdapter(adapter)
        }



        val buttonAccept = rootView.findViewById<Button>(R.id.addPonderations)
        buttonAccept.setOnClickListener{

            val index = newList.indexOf(spinerSubjects.selectedItem)
            val subject = listSubjects.get(index)

            var exist = false
            for(element in view.listCoursedSubjects){
                if(subject.id==element.subjectId){
                    exist=true
                    break
                }
            }

            if(!exist) {

                var trimestre = 1
                val stringTrim = editTrim.text.toString()
                if (stringTrim != "") {
                    trimestre = stringTrim.toInt()
                }


                val addedSubject = Student_Subject_Provisional(0, idStudent, subject.id, trimestre, subject.nombre, null)

                GlobalScope.launch {
                    presenter.insertSubjectToStudent(addedSubject)
                    delay(200)
                    presenter.updateNames()
                    presenter.getCoursedSubject()
                }
            }
            dialog?.dismiss()



        }


        return rootView
    }
}