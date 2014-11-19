<p><strong>Project Description</strong><br />Introduced by Alonzo Church and his student Stephen Cole Kleene in the 1930s to <br />study computable functions.<br />A (very simple) formal system for defining functions and their operational meanings,<br />yet is shown to be as powerful as other systems.<br /><br />This project aims to be a journey of discovery from the foundations of computing to the first commercial functional language on an industrial strength platform.<br /><br />Blog: <a href="http://fsharpcode.blogspot.com/">http://fsharpcode.blogspot.com/</a><br /><br />Recommended Reading<br /><a href="http://www.amazon.co.uk/gp/product/0596153643?ie=UTF8&amp;tag=httpfsharpcbl-21&amp;linkCode=as2&amp;camp=1634&amp;creative=6738&amp;creativeASIN=0596153643">Programming F#</a><br /><a href="http://www.amazon.co.uk/gp/product/0521114292?ie=UTF8&amp;tag=httpfsharpcbl-21&amp;linkCode=as2&amp;camp=1634&amp;creative=6738&amp;creativeASIN=0521114292">Lambda-Calculus, Combinators, and Functional Programming</a><br /><br />Lambda Calculus Interpreter:<br /><br />Identity</p>
<pre>Lambda Calculus interpreter 0.0.0.0
&gt; (&lambda;x.x) (&lambda;y.y)
(&lambda;y.y)
</pre>
<p><br />Church Numerals</p>
<pre>Lambda Calculus interpreter 0.0.0.0
&gt; (&lambda;f.&lambda;x.x)
(&lambda;f.(&lambda;x.x))
&gt; (&lambda;n.&lambda;f.&lambda;x.(f (n f x))) (&lambda;f.&lambda;x.x)
(&lambda;f.(&lambda;x.(f x)))
&gt; (&lambda;n.&lambda;f.&lambda;x.(f (n f x))) (&lambda;f.&lambda;x.(f x))
(&lambda;f.(&lambda;x.(f (f x))))
&gt; (&lambda;n.&lambda;f.&lambda;x.(f (n f x))) (&lambda;f.&lambda;x.(f (f x)))
(&lambda;f.(&lambda;x.(f (f (f x)))))
&gt; (&lambda;n.&lambda;f.&lambda;x.(f (n f x))) (&lambda;f.&lambda;x.(f (f (f x))))
(&lambda;f.(&lambda;x.(f (f (f (f x))))))
</pre>
<p><br />Addition</p>
<pre>Lambda Calculus interpreter 0.0.0.0
&gt; (&lambda;m.&lambda;n.&lambda;f.&lambda;x.((m f) (n f x))) (&lambda;h.&lambda;x.(h (h x))) (&lambda;g.&lambda;x.(g (g (g x))))
(&lambda;f.(&lambda;x.(f (f (f (f (f x)))))))
&gt; (&lambda;m.&lambda;n.&lambda;f.&lambda;x.((m f) (n f x))) (&lambda;h.&lambda;x.(h (h (h (h x))))) (&lambda;g.&lambda;x.(g (g (g (g (g (g x)))))))
(&lambda;f.(&lambda;x.(f (f (f (f (f (f (f (f (f (f x))))))))))))
</pre>
