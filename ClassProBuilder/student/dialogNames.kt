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

class dialogNames(val nameSelected : String, val surnameSelected : String, val selectedId : Int , val presenter : presenterStudents) : DialogFragment(){

    override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View? {

        var rootView : View = inflater.inflate(R.layout.dialog_studentsubject_selected,container,false)

        val name = rootView.findViewById<TextView>(R.id.nameToShowData)
        name.text = nameSelected+" "+surnameSelected


        val changeActivity = rootView.findViewById<Button>(R.id.goButton)
        changeActivity.setOnClickListener{

            dialog?.dismiss()

            val intent = Intent(this.context, student_signature::class.java).apply {
                putExtra("nameStudent", nameSelected+" "+surnameSelected)
                Log.d("Testeo id 3", selectedId.toString() )
                putExtra("id", selectedId )
            }
            startActivity(intent)
        }


        val deleteElement = rootView.findViewById<Button>(R.id.deleteButton)
        deleteElement.setOnClickListener{

            dialog?.dismiss()
            GlobalScope.launch {
                presenter.deleteStudent(nameSelected)
                delay(200)
                presenter.updateNames()
            }



        }

        return rootView
    }

}