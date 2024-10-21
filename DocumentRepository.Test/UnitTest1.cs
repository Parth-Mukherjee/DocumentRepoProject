namespace DocumentRepository.Test
{
    public class UnitTest1
    {

        [Fact]
        public void Test1()
        {
            int num1 = 10; int num2 = 20;
            int expected = num1 + num2;
            Math mymath = new Math();
            var actual = mymath.Add(num1,num2);
            Assert.Equal(expected, actual);

        }
    }
}