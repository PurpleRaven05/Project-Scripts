package com.jorgebg.classprobuilder.subject

import android.content.Intent
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import androidx.fragment.app.DialogFragment
import com.jorgebg.classprobuilder.R
import com.jorgebg.classprobuilder.student.student_signature
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch

class dialogDeletePercentage(val nameShown : String, val idSel : Int ,val presenter : presenterSubjectPercentaje): DialogFragment() {
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?

    ): View? {

        var rootView : View = inflater.inflate(R.layout.delete_ponderation,container,false)

        val NamePond = rootView.findViewById<TextView>(R.id.nameToShowData)
        NamePond.text=nameShown

        val deletePond = rootView.findViewById<Button>(R.id.deleteButton)
        deletePond.setOnClickListener{

            GlobalScope.launch {
                presenter.deletePonderation(idSel)
                delay(200)
                presenter.updatePercentageRecycler()
            }

            dialog?.dismiss()



        }


        return rootView
    }
}