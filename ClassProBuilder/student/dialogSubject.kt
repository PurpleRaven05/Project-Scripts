package com.jorgebg.classprobuilder.student

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import androidx.fragment.app.DialogFragment
import com.jorgebg.classprobuilder.R
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch

class dialogSubject(val nameSelected : String, val subjectSelected : String,val idSubjectStud : Int, val idSubject : Int , val idStud : Int,val presenter : presenterStudentSubject) : DialogFragment(){

    override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View? {

        var rootView : View = inflater.inflate(R.layout.dialog_studentsubject_selected,container,false)

        val name = rootView.findViewById<TextView>(R.id.nameToShowData)
        name.text = subjectSelected


        val changeActivity = rootView.findViewById<Button>(R.id.goButton)
        changeActivity.setOnClickListener{

            GlobalScope.launch {
                presenter.checkPonderations(idSubject)
                delay(50)

                if(presenter.hasPons) {

                val intent = Intent(context, student_marks::class.java).apply {
                    putExtra("nameStudent", nameSelected)
                    putExtra("nameSubject", subjectSelected)
                    putExtra("idSubjectStudent", idSubjectStud)
                    putExtra("idSubject", idSubject)
                    putExtra("idStudent", idStud)
                    }
                    startActivity(intent)
                }
                presenter.hasPons=true
                dialog?.dismiss()
            }
        }

        val deleteSubjectStudent = rootView.findViewById<Button>(R.id.deleteButton)
        deleteSubjectStudent.setOnClickListener{

            GlobalScope.launch {
                presenter.deleteSubjectStudent(idSubjectStud)
                delay(200)
                presenter.updateNames()
            }

            dialog?.dismiss()
        }


        return rootView
    }
}