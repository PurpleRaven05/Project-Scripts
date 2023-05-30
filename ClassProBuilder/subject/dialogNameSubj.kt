package com.jorgebg.classprobuilder.subject

import android.content.Intent
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

class dialogNameSubj(val subjectSelected : String, val subjectID:Int , val presenter : presenterSubject) : DialogFragment(){

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

            dialog?.dismiss()
            val intent = Intent(this.context, subjectPercentage::class.java).apply {
                putExtra("nameSubject", subjectSelected)
                putExtra("idSubject", subjectID)
            }
            startActivity(intent)
        }

        val deleteElement = rootView.findViewById<Button>(R.id.deleteButton)
        deleteElement.setOnClickListener{

            dialog?.dismiss()

            GlobalScope.launch {
                presenter.deleteSubject(subjectID)
                delay(200)
                presenter.updateSubjectRecycler()
            }





        }

        return rootView
    }
}