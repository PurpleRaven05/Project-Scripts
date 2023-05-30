package com.jorgebg.classprobuilder.subject

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.fragment.app.DialogFragment
import com.jorgebg.classprobuilder.R
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch

class dialogInsertPonderation(val presenter : presenterSubjectPercentaje):DialogFragment() {
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?

    ): View? {

        var rootView : View = inflater.inflate(R.layout.insert_ponderation,container,false)


        val editNamePond = rootView.findViewById<EditText>(R.id.editPondName)
        val editWeightPond = rootView.findViewById<EditText>(R.id.editPondWeight)

        val changeActivity = rootView.findViewById<Button>(R.id.acceptPond)
        changeActivity.setOnClickListener{

            var namePond= editNamePond.text.toString()
            var weightPond= editWeightPond.text.toString()
            var weightFloat = 0f
            var voidString = ""

            Log.d("Testeo", weightPond)

            if(namePond != voidString && weightPond.isNotEmpty()){
                GlobalScope.launch {
                    weightFloat = weightPond.toFloat()
                    presenter.insertPonderation(namePond,weightFloat)
                    delay(200)
                    presenter.updatePercentageRecycler()
                }

                
            }
            else
                GlobalScope.launch {
                    delay(200)
                    presenter.insertPonderation("Sample",0f)
                    presenter.updatePercentageRecycler()
                }




            dialog?.dismiss()
        }

        return rootView
    }
}