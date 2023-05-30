package com.jorgebg.classprobuilder.student

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.EditText
import androidx.fragment.app.DialogFragment
import com.jorgebg.classprobuilder.R
import com.jorgebg.classprobuilder.databases.StudentProvisional
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch

class dialogInsertName(val presenter: presenterStudents) : DialogFragment(){

    override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?

    ): View? {
        var rootView : View = inflater.inflate(R.layout.insert_student,container,false)


        val name =rootView.findViewById<EditText>(R.id.editName)
        val firstSur =rootView.findViewById<EditText>(R.id.editSubject)
        val secondSur =rootView.findViewById<EditText>(R.id.editSurename2)

        val changeActivity = rootView.findViewById<Button>(R.id.addPonderations)


        changeActivity.setOnClickListener{

            var nameText = name.text.toString()
            var surname1Text = firstSur.text.toString()
            var surname2Text = secondSur.text.toString()

            if(nameText != "" && surname1Text != ""){
                GlobalScope.launch {
                    presenter.insertStudent(StudentProvisional(0,nameText,surname1Text,surname2Text))
                    delay(200)
                    presenter.updateNames()
                }
            }
            dialog?.dismiss()

        }

        return rootView
    }
}