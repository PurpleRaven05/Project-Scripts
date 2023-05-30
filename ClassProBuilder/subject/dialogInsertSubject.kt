package com.jorgebg.classprobuilder.subject

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.EditText
import androidx.fragment.app.DialogFragment
import com.jorgebg.classprobuilder.R
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch

class dialogInsertSubject(val presenter: presenterSubject) : DialogFragment(){

    override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?

    ): View? {

        var rootView : View = inflater.inflate(R.layout.select_subject,container,false)


        val editNameSubject = rootView.findViewById<EditText>(R.id.editTextSubject)
        val changeActivity = rootView.findViewById<Button>(R.id.acceptButton)

        changeActivity.setOnClickListener{


            var nameSubj= editNameSubject.text.toString()

            if(nameSubj.isNotEmpty()) {

                GlobalScope.launch {

                    presenter.insertSubject(nameSubj)
                    delay(200)
                    presenter.updateSubjectRecycler()
                }

            }


            dialog?.dismiss()
        }

        return rootView
    }
}