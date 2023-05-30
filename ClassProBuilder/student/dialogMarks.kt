package com.jorgebg.classprobuilder.student

import android.os.Bundle
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

class dialogMarks(val nameToShow : String, val idStudentSubject:Int, val chosenID: Int, val presenter : presenterStudentMarks) : DialogFragment(){

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        var rootView : View = inflater.inflate(R.layout.delete_ponderation,container,false)


        val name = rootView.findViewById<TextView>(R.id.nameToShowData)
        name.text = nameToShow

        val deleteElement = rootView.findViewById<Button>(R.id.deleteButton)
        deleteElement.setOnClickListener{

            dialog?.dismiss()
            GlobalScope.launch {
                presenter.deleteStudentPonderation(chosenID)
                delay(200)
                presenter.updateMarks()
            }



        }

        return rootView
    }
}