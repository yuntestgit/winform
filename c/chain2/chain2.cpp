#include <iostream>
template <class T> class Chain;

template <class T>
class ChainNode
{
	friend class Chain<T>;
	public:
	ChainNode(const T& element, ChainNode *next=0)
	{
		data=element;
		link=next;
	}
	private:
	char data;
	ChainNode *link;
};

template <class T>
class Chain
{
	public:
	Chain() { first=0; }
	
	void InsertBack(const T& e)
	{
		if(first)
		{
			last->link = new ChainNode<T>(e);
			last=last->link;
		}
		else
		{
			first=last=new ChainNode<T>(e);
		}
	}
	
	void Concatenate(Chain<T>& b)
	{
		if(first)
		{
			last->link = b.first;
			last=b.last;
		}
		else
		{
			first=b.first;
			last=b.last;
		}
		b.first=b.last=0;
	}
	
	void Reverse()
	{
		ChainNode<T> *current=first, *previous=0;
		while(current)
		{
			ChainNode<T> *r = previous;
			previous = current;
			current = current->link;
			previous->link=r;
		}
		first=previous;
	}
	
	void print()
	{
		ChainIterator ptr = begin();
		while(ptr!=end())
		{
			std::cout<<*ptr;
			ptr++;
		}
		std::cout<<std::endl;
	}
	
	private:
	ChainNode<T> *first, *last;
	
	class ChainIterator
	{
		public:
		ChainIterator(ChainNode<T>* startNode=0) { current=startNode; }
		T& operator*() const { return current->data; }
		T* operator->() const { return &current->data; }
		ChainIterator& operator++() { current=current->link; return *this; }
		ChainIterator operator++(int)
		{
			ChainIterator old = *this;
			current=current->link;
			return old;
		}
		bool operator!=(const ChainIterator right) const {return current!=right.current;}
		bool operator==(const ChainIterator right) const {return current==right.current;}
		
		private:
		ChainNode<T>* current;
	};
	
	public:
	ChainIterator begin() { return ChainIterator(first); }
	ChainIterator end() { return ChainIterator(0); }
};

int main(int argc, char** argv) {
	Chain<char> example1, example2;
	std::string strHello="hello", strWorld="world";
	
	for(int i=0; i<strHello.length(); i++)
		example1.InsertBack(strHello[i]);
	example1.print();
	
	for(int i=0; i<strWorld.length(); i++)
		example2.InsertBack(strWorld[i]);
	example2.print();
	
	example1.Concatenate(example2);
	example1.print();
	
	example1.Reverse();
	example1.print();
	return 0;
}
